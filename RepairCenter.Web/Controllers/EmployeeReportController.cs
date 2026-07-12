using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairCenter.Services.EmployeeReports;
using RepairCenter.Services.EmployeeReports.Dtos;

namespace RepairCenter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class EmployeeReportController : ControllerBase
    {
        private readonly IEmployeeReportService _employeeReportService;

        public EmployeeReportController(
            IEmployeeReportService employeeReportService)
        {
            _employeeReportService = employeeReportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReport(
            [FromQuery] EmployeeReportFilterDto dto)
        {
            var result = await _employeeReportService
                .GetReportAsync(dto);

            return Ok(result);
        }
    }
}