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


            // =====================================================
            // ADMIN FIRST REVIEW
            // =====================================================

            public async Task AdminReviewAsync(
                AdminReviewDto dto,
                string userId)
            {
                var request = await _context.ServiceRequests
                    .FirstOrDefaultAsync(x =>
                        x.Id == dto.RequestId);

                if (request == null)
                    throw new Exception("Request Not Found");


                request.ProblemCause = dto.ProblemCause;

                request.Cost = dto.Cost;

                request.ExpectedDays = dto.ExpectedDays;


                // ============================================
                // Add Admin Note
                // ============================================

                if (!string.IsNullOrWhiteSpace(dto.Note))
                {
                    var note = new RequestNote
                    {
                        ServiceRequestId = request.Id,
                        Note = dto.Note,
                        CreatedById = userId,
                        Status = RequestStatus.WaitingCustomerApproval
                    };

                    _context.RequestNotes.Add(note);
                }


                // ============================================
                // Status
                // ============================================

                request.Status =
                    RequestStatus.WaitingCustomerApproval;


                await _context.SaveChangesAsync();


                // ============================================
                // Notification
                // ============================================

                await _notificationService.CreateForAllEmployeesAsync(
                    NotificationType.WaitingCustomerApproval,
                    request.Id,
                    request.RequestNumber);
            }


            // =====================================================
            // COMPANY REJECT
            // =====================================================

            public async Task RejectByCompanyAsync(
                CompanyRejectDto dto)
            {
                var request = await _context.ServiceRequests
                    .FirstOrDefaultAsync(x =>
                        x.Id == dto.RequestId);

                if (request == null)
                    throw new Exception("Request Not Found");


                request.RejectionReason =
                    dto.RejectionReason;

                request.Status =
                    RequestStatus.CompanyRejected;


                await _context.SaveChangesAsync();


                await _notificationService.CreateForAllEmployeesAsync(
                    NotificationType.RequestRejectedByAdmin,
                    request.Id,
                    request.RequestNumber);
            }


            // =====================================================
            // REPRICING REQUESTED → ADMIN
            // =====================================================

            public async Task RepriceAsync(
                RepricingDto dto,
                string userId)
            {
                var request = await _context.ServiceRequests
                    .FirstOrDefaultAsync(x =>
                        x.Id == dto.RequestId);

                if (request == null)
                    throw new Exception("Request Not Found");


                // ============================================
                // Make sure this request actually needs repricing
                // ============================================

                if (request.Status != RequestStatus.RepricingRequested)
                {
                    throw new Exception(
                        "This request is not waiting for repricing.");
                }


                // ============================================
                // Update Price
                // ============================================

                request.Cost = dto.Cost;


                // ============================================
                // Validate Admin Decision
                // ============================================

                if (dto.Status != RequestStatus.WaitingCustomerApproval &&
                    dto.Status != RequestStatus.CompanyRejected)
                {
                    throw new Exception(
                        "Invalid status for repricing.");
                }


                // ============================================
                // Update Request Status
                // ============================================

                request.Status = dto.Status;


                // ============================================
                // Add Repricing History Note
                // ============================================

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


                await _context.SaveChangesAsync();


                // ============================================
                // Notifications
                // ============================================

                if (dto.Status ==
                    RequestStatus.WaitingCustomerApproval)
                {
                    await _notificationService.CreateForAllEmployeesAsync(
                        NotificationType.WaitingCustomerApproval,
                        request.Id,
                        request.RequestNumber);
                }


                if (dto.Status ==
                    RequestStatus.CompanyRejected)
                {
                    await _notificationService.CreateForAllEmployeesAsync(
                        NotificationType.RequestRejectedByAdmin,
                        request.Id,
                        request.RequestNumber);
                }
            }
        }
    }
}