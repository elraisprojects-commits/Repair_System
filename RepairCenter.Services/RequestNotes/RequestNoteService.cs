using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RepairCenter.data.Contexts;
using RepairCenter.data.Entities;
using RepairCenter.data.Enums;
using RepairCenter.Services.Notification;
using RepairCenter.Services.RequestNotes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.RequestNotes
{
    public class RequestNoteService : IRequestNoteService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        public RequestNoteService(
    AppDbContext context,
    IMapper mapper,
    INotificationService notificationService)
        {
            _context = context;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task AddNoteAsync(
     AddRequestNoteDto dto,
     string userId)
        {
            var request = await _context.ServiceRequests
                .FirstOrDefaultAsync(x => x.Id == dto.RequestId);

            if (request == null)
                throw new Exception("Request not found.");

            // لو الطلب لسه مفيش له Specialist
            // نربطه بأول موظف كتب Note
            if (string.IsNullOrEmpty(request.SpecialistId))
            {
                request.SpecialistId = userId;
            }

            // لو الـ Note جاية معاها Status
            if (dto.Status.HasValue)
            {
                if (dto.Status != RequestStatus.WaitingCustomerApproval &&
                    dto.Status != RequestStatus.InProgress &&
                    dto.Status != RequestStatus.Completed &&
                    dto.Status != RequestStatus.CancelledByCustomer)
                {
                    throw new Exception("Invalid Status.");
                }

                // تحديث حالة الطلب
                request.Status = dto.Status.Value;
            }

            // إنشاء الـ Note
            var note = _mapper.Map<RequestNote>(dto);

            note.CreatedById = userId;

            _context.RequestNotes.Add(note);

            await _context.SaveChangesAsync();

            // ============================
            // Notifications
            // ============================

            // Note فقط بدون تغيير Status
            if (!dto.Status.HasValue)
            {
                await _notificationService.CreateForAllEmployeesAsync(
                    NotificationType.RequestUpdated,
                    request.Id,
                    request.RequestNumber);

                return;
            }

            // Status اتغير
            switch (dto.Status.Value)
            {
                case RequestStatus.InProgress:

                    await _notificationService.CreateForAllEmployeesAsync(
                        NotificationType.CustomerApproved,
                        request.Id,
                        request.RequestNumber);

                    break;

                case RequestStatus.CancelledByCustomer:

                    await _notificationService.CreateForAllEmployeesAsync(
                        NotificationType.CustomerRejected,
                        request.Id,
                        request.RequestNumber);

                    break;

                case RequestStatus.Completed:

                    await _notificationService.CreateForAllEmployeesAsync(
                        NotificationType.RequestCompleted,
                        request.Id,
                        request.RequestNumber,
                        includeReceptionist: true);

                    break;
            }
        }



        // GET NOTES FOR REQUEST
        // ============================================

        public async Task<List<RequestNoteDto>> GetByRequestIdAsync(
            int requestId)
        {
            var notes = await _context.RequestNotes
                .Include(x => x.CreatedBy)
                .Where(x => x.ServiceRequestId == requestId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return _mapper.Map<List<RequestNoteDto>>(notes);
        }
    }
 }
