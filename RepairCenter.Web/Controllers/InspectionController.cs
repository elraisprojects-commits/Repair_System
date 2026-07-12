using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairCenter.Services.Inspection;
using RepairCenter.Services.Inspection.Dtos;
using System.Security.Claims;

namespace RepairCenter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InspectionController : ControllerBase
    {
        private readonly IInspectionService _inspectionService;

        public InspectionController(
            IInspectionService inspectionService)
        {
            _inspectionService = inspectionService;
        }

        [HttpPut]
        [Authorize(Roles = "Specialist")]
        public async Task<IActionResult> UpdateInspectionResult(
            UpdateInspectionResultDto dto)
        {
            var specialistId =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

            if (specialistId == null)
                return Unauthorized();

            await _inspectionService
                .UpdateInspectionResultAsync(
                    dto);

            return Ok(new
            {
                Message = "Inspection Result Saved Successfully"
            });
        }
    }
}