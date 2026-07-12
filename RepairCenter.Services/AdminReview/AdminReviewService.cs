using Microsoft.EntityFrameworkCore;
using RepairCenter.data.Contexts;
using RepairCenter.data.Entities;
using RepairCenter.data.Enums;
using RepairCenter.Services.AdminReview.Dtos;
using RepairCenter.Services.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.AdminReview
{


    namespace RepairCenter.Services.AdminReviews
    {
        public class AdminReviewService : IAdminReviewService
        {
            private readonly AppDbContext _context;

            private readonly INotificationService _notificationService;

            public AdminReviewService(
                AppDbContext context,
                INotificationService notificationService)
            {
                _context = context;
                _notificationService = notificationService;
            }
            public async Task AdminReviewAsync(
                AdminReviewDto dto, string userId)
            {
                var request = await _context.ServiceRequests
                    .FirstOrDefaultAsync(x =>
                        x.Id == dto.RequestId);

                if (request == null)
                    throw new Exception("Request Not Found");

                request.ProblemCause = dto.ProblemCause;

                request.Cost = dto.Cost;

                request.ExpectedDays = dto.ExpectedDays;


                if (!string.IsNullOrWhiteSpace(dto.Note))
                {
                    var note = new RequestNote
                    {
                        ServiceRequestId = request.Id,
                        Note = dto.Note,
                        CreatedById = userId,
                        Status = dto.Status
                    };

                    _context.RequestNotes.Add(note);
                }

                request.Status =
                    RequestStatus.WaitingCustomerApproval;

                await _context.SaveChangesAsync();

                await _notificationService.CreateForAllEmployeesAsync(
                  NotificationType.WaitingCustomerApproval,
                  request.Id,
                  request.RequestNumber);
            }


            public async Task RejectByCompanyAsync(
                  CompanyRejectDto dto)
            {
                var request = await _context.ServiceRequests
                    .FirstOrDefaultAsync(x => x.Id == dto.RequestId);

                if (request == null)
                    throw new Exception("Request Not Found");

                request.RejectionReason = dto.RejectionReason;

                request.Status = RequestStatus.CompanyRejected;

                await _context.SaveChangesAsync();

                await _notificationService.CreateForAllEmployeesAsync(
                   NotificationType.RequestRejectedByAdmin,
                   request.Id,
                   request.RequestNumber);
            }
        }
    }
}