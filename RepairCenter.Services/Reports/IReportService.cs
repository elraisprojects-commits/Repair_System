using RepairCenter.Services.Reports.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Reports
{
    public interface IReportService
    {
       // Task<DashboardDto> GetDashboardAsync();
        Task<DashboardDto> GetDashboardAsync(DashboardFilterDto dto);
    }
}