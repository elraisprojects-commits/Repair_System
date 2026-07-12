using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepairCenter.data.Contexts;
using RepairCenter.data.Entities;
using RepairCenter.data.Enums;
using RepairCenter.Services.Interfaces;
using RepairCenter.Services.Notification.Dtos;
using RepairCenter.Services.Notification.Dtos.RepairCenter.Services.Notifications.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotificationEntity = RepairCenter.data.Entities.Notification;

namespace RepairCenter.Services.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly INotificationSender _notificationSender;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificationService(
      AppDbContext context,
      IMapper mapper,
      INotificationSender notificationSender,
      UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _notificationSender = notificationSender;
            _userManager = userManager;
        }
        public async Task CreateAsync(CreateNotificationDto dto)
        {
            var notification = new NotificationEntity
            {
                UserId = dto.UserId,

                Title = dto.Title,

                Message = dto.Message,

                Type = dto.Type,

                RequestId = dto.RequestId,

                IsRead = false
            };

            _context.Notifications.Add(notification);

            await _context.SaveChangesAsync();

            var notificationDto = _mapper.Map<NotificationDto>(notification);

            await _notificationSender.SendAsync(
                dto.UserId,
                notificationDto);
        }

                    public async Task<List<NotificationDto>> GetMyNotificationsAsync(
            string userId,
            NotificationFilterDto? filter)
        {
            var query = _context.Notifications
                .Where(x => x.UserId == userId)
                .AsQueryable();

            if (filter != null)
            {
                if (filter.IsRead.HasValue)
                {
                    query = query.Where(x =>
                        x.IsRead == filter.IsRead.Value);
                }

                if (filter.Type.HasValue)
                {
                    query = query.Where(x =>
                        x.Type == filter.Type.Value);
                }
            }

            var notifications = await query

                .OrderByDescending(x => x.CreatedAt)

                .ToListAsync();

            return _mapper.Map<List<NotificationDto>>(notifications);
        }

        public async Task<NotificationCountDto> GetUnreadCountAsync(
            string userId)
        {
            var count = await _context.Notifications
                .CountAsync(x =>
                    x.UserId == userId &&
                    !x.IsRead);

            return new NotificationCountDto
            {
                UnReadCount = count
            };
        }

        public async Task MarkAsReadAsync(
            int notificationId,
            string userId)
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(x =>
                    x.Id == notificationId &&
                    x.UserId == userId);

            if (notification == null)
                throw new Exception("Notification not found.");

            if (!notification.IsRead)
            {
                notification.IsRead = true;

                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkAllAsReadAsync(
            string userId)
        {
            var notifications = await _context.Notifications
                .Where(x =>
                    x.UserId == userId &&
                    !x.IsRead)
                .ToListAsync();

            if (!notifications.Any())
                return;

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(
            int notificationId,
            string userId)
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(x =>
                    x.Id == notificationId &&
                    x.UserId == userId);

            if (notification == null)
                throw new Exception("Notification not found.");

            _context.Notifications.Remove(notification);

            await _context.SaveChangesAsync();
        }

        public async Task CreateForAllEmployeesAsync(
     NotificationType type,
     int requestId,
     string requestNumber,
     bool includeReceptionist = false)
        {
            var users = new List<ApplicationUser>();

            users.AddRange(await _userManager.GetUsersInRoleAsync("Admin"));

            users.AddRange(await _userManager.GetUsersInRoleAsync("Specialist"));

            if (includeReceptionist)
            {
                users.AddRange(
                    await _userManager.GetUsersInRoleAsync("Receptionist"));
            }

            users = users
                .GroupBy(x => x.Id)
                .Select(x => x.First())
                .ToList();

            var notification = BuildNotification(type, requestNumber);

            foreach (var user in users)
            {
                await CreateAsync(new CreateNotificationDto
                {
                    UserId = user.Id,

                    Title = notification.Title,

                    Message = notification.Message,

                    Type = type,

                    RequestId = requestId
                });
            }
        }

        private (string Title, string Message) BuildNotification(
    NotificationType type,
    string requestNumber)
        {
            return type switch
            {
                NotificationType.RequestCreated => (
                    "New Request",
                    $"Request {requestNumber} has been created."
                ),

                NotificationType.InspectionSubmitted => (
                    "Inspection Submitted",
                    $"Inspection has been submitted for request {requestNumber}."
                ),

                NotificationType.WaitingCustomerApproval => (
                    "Waiting Customer Approval",
                    $"Request {requestNumber} is waiting for customer approval."
                ),

                NotificationType.WaitingCustomerResponse => (
                    "Waiting Customer Response",
                    $"Waiting for customer response for request {requestNumber}."
                ),

                NotificationType.CustomerApproved => (
                    "Customer Approved",
                    $"Customer approved request {requestNumber}."
                ),

                NotificationType.CustomerRejected => (
                    "Customer Rejected",
                    $"Customer rejected request {requestNumber}."
                ),

                NotificationType.RequestRejectedByAdmin => (
                    "Request Rejected",
                    $"Request {requestNumber} was rejected by admin."
                ),

                NotificationType.RequestUpdated => (
                    "Request Updated",
                    $"Request {requestNumber} has been updated."
                ),

                NotificationType.RequestCompleted => (
                    "Request Completed",
                    $"Request {requestNumber} has been completed."
                ),

                NotificationType.RequestDelivered => (
                    "Request Delivered",
                    $"Request {requestNumber} has been delivered."
                ),

                _ => (
                    "Notification",
                    "You have a new notification."
                )
            };
        }
    }
}
