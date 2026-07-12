using RepairCenter.Services.EmployeeReports.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.EmployeeReports
{
    public interface IEmployeeReportService
    {
        Task<EmployeeReportResponseDto> GetReportAsync(
            EmployeeReportFilterDto dto);
    }
}