using RepairCenter.Services.RequestReports.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.RequestReports
{
    public interface IRequestReportService
    {
        Task<List<RequestReportDto>> GetReportAsync(
            RequestReportFilterDto dto);
    }
}
