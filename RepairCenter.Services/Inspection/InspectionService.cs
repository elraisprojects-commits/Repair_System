using Microsoft.EntityFrameworkCore;
using RepairCenter.data.Contexts;
using RepairCenter.data.Enums;
using RepairCenter.Services.Inspection.Dtos;
using RepairCenter.Services.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Inspection
{
    public class InspectionService : IInspectionService
    {
        private readonly AppDbContext _context;

        private readonly INotificationService _notificationService;

        public InspectionService(
            AppDbContext context,
            INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }
        public async Task UpdateInspectionResultAsync(
    UpdateInspectionResultDto dto)
        {
            var request = await _context.ServiceRequests
                .FirstOrDefaultAsync(x =>
                    x.Id == dto.RequestId);

            if (request == null)
                throw new Exception("Request Not Found");

            request.InspectionResult = dto.InspectionResult;

            request.Status = RequestStatus.UnderReview;

            await _context.SaveChangesAsync();

            await _notificationService.CreateForAllEmployeesAsync(
    NotificationType.InspectionSubmitted,
    request.Id,
    request.RequestNumber);
        }
    }
}
