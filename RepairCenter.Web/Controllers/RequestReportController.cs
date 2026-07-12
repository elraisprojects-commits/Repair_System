using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairCenter.Services.RequestReports;
using RepairCenter.Services.RequestReports.Dtos;

namespace RepairCenter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   [Authorize(Roles = "Admin")]
    public class RequestReportController : ControllerBase
    {
        private readonly IRequestReportService _requestReportService;

        public RequestReportController(
            IRequestReportService requestReportService)
        {
            _requestReportService = requestReportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReport(
            [FromQuery] RequestReportFilterDto dto)
        {
            var result = await _requestReportService.GetReportAsync(dto);

            return Ok(result);
        }
    }
}