using Microsoft.EntityFrameworkCore;
using RepairCenter.data.Contexts;
using RepairCenter.data.Enums;
using RepairCenter.Services.DeliverRequest.Dtos;
using RepairCenter.Services.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.DeliverRequest
{
    public class DeliveryService : IDeliveryService
    {
        private readonly AppDbContext _context;

        private readonly INotificationService _notificationService;

        public DeliveryService(
            AppDbContext context,
            INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }
        public async Task DeliverAsync(
            DeliverRequestDto dto,
            string userId)
        {
            var request = await _context.ServiceRequests
                .FirstOrDefaultAsync(x => x.Id == dto.RequestId);

            if (request == null)
                throw new Exception("Request not found.");

            if (request.Status != RequestStatus.Completed)
                throw new Exception("Only completed requests can be delivered.");

            request.Status = RequestStatus.Delivered;

            request.DeliveredById = userId;

            request.DeliveredByName = dto.DeliveredByName;

            request.DeliveredAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            await _notificationService.CreateForAllEmployeesAsync(
    NotificationType.RequestDelivered,
    request.Id,
    request.RequestNumber,
    includeReceptionist: true);
        }
    }
}
