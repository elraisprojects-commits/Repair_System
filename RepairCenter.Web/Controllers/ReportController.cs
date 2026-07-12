using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairCenter.Services.Reports;
using RepairCenter.Services.Reports.Dtos;

namespace RepairCenter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     [Authorize(Roles = "Admin")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard(
             [FromQuery] DashboardFilterDto dto)
        {
            var result =
                await _reportService.GetDashboardAsync(dto);

            return Ok(result);
        }
    }
}