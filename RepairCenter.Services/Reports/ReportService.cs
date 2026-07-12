using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepairCenter.data.Contexts;
using RepairCenter.data.Entities;
using RepairCenter.data.Enums;
using RepairCenter.Services.Reports.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Reports
{
    namespace RepairCenter.Services.Dashboard
    {
        public class ReportService: IReportService
        {
            private readonly AppDbContext _context;
            private readonly UserManager<ApplicationUser> _userManager;

            public ReportService(
                AppDbContext context,
                UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _userManager = userManager;
            }

            public async Task<DashboardDto> GetDashboardAsync(
                DashboardFilterDto dto)
            {
                var dashboard = new DashboardDto();

                var requests = _context.ServiceRequests.AsQueryable();

                // ============================
                // Date Filter
                // ============================

                if (dto.FromDate.HasValue)
                {
                    requests = requests.Where(x =>
                        x.CreatedAt.Date >= dto.FromDate.Value.Date);
                }

                if (dto.ToDate.HasValue)
                {
                    requests = requests.Where(x =>
                        x.CreatedAt.Date <= dto.ToDate.Value.Date);
                }

                // ============================
                // Requests
                // ============================

                dashboard.TotalRequests =
                    await requests.CountAsync();

                dashboard.Received =
                    await requests.CountAsync(x =>
                        x.Status == RequestStatus.Received);

                dashboard.UnderReview =
                    await requests.CountAsync(x =>
                        x.Status == RequestStatus.UnderReview);

                dashboard.WaitingCustomerApproval =
                    await requests.CountAsync(x =>
                        x.Status == RequestStatus.WaitingCustomerApproval);

                dashboard.InProgress =
                    await requests.CountAsync(x =>
                        x.Status == RequestStatus.InProgress);

                dashboard.Completed =
                    await requests.CountAsync(x =>
                        x.Status == RequestStatus.Completed);

                dashboard.Delivered =
                    await requests.CountAsync(x =>
                        x.Status == RequestStatus.Delivered);

                dashboard.CompanyRejected =
                    await requests.CountAsync(x =>
                        x.Status == RequestStatus.CompanyRejected);

                dashboard.CancelledByCustomer =
                    await requests.CountAsync(x =>
                        x.Status == RequestStatus.CancelledByCustomer);

                dashboard.TotalCancelled =
                    dashboard.CompanyRejected +
                    dashboard.CancelledByCustomer;

                // ============================
                // Customers
                // خلال الفترة المحددة
                // ============================

                dashboard.TotalCustomers =
                    await requests
                        .Select(x => x.CustomerId)
                        .Distinct()
                        .CountAsync();

                // ============================
                // Devices
                // خلال الفترة المحددة
                // ============================

                dashboard.TotalDevices =
                    await requests
                        .Select(x => x.DeviceId)
                        .Distinct()
                        .CountAsync();

                // ============================
                // Employees
                // ============================

                dashboard.TotalEmployees =
                    await _context.Users.CountAsync();

                dashboard.TotalAdmins =
                    (await _userManager
                        .GetUsersInRoleAsync("Admin"))
                    .Count;

                dashboard.TotalReceptionists =
                    (await _userManager
                        .GetUsersInRoleAsync("Receptionist"))
                    .Count;

                dashboard.TotalSpecialists =
                    (await _userManager
                        .GetUsersInRoleAsync("Specialist"))
                    .Count;
                // ============================
                // Inventory
                // ============================

                dashboard.TotalInventoryItems =
      await _context.InventoryItem
          .Where(x => x.IsActive)
          .CountAsync();

                dashboard.OutOfStockItems =
                    await _context.InventoryItem
                        .Where(x => x.IsActive &&
                                    x.Quantity == 0)
                        .CountAsync();

                dashboard.TotalInventoryValue =
                    await _context.InventoryItem
                        .Where(x => x.IsActive)
                        .SumAsync(x => x.TotalPrice);

                // ============================
                // Revenue
                // خلال الفترة المحددة
                // ============================

                dashboard.TotalRevenue =
                    await requests
                        .Where(x =>
                            x.Status == RequestStatus.Delivered)
                        .SumAsync(x => x.Cost ?? 0);

                dashboard.AverageRepairCost =
                    await requests
                        .Where(x =>
                            x.Status == RequestStatus.Delivered &&
                            x.Cost != null)
                        .AverageAsync(x => (decimal?)x.Cost) ?? 0;

                return dashboard;
            }
        }
    }
}