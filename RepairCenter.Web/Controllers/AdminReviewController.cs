using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairCenter.Services.AdminReview;
using RepairCenter.Services.AdminReview.Dtos;
using System.Security.Claims;

namespace RepairCenter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminReviewController : ControllerBase
    {
        private readonly IAdminReviewService _adminReviewService;

        public AdminReviewController(
            IAdminReviewService adminReviewService)
        {
            _adminReviewService = adminReviewService;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Review(
            AdminReviewDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            await _adminReviewService.AdminReviewAsync(dto, userId);

            return Ok(new
            {
                Message = "Review Saved Successfully"
            });
        }


        [HttpPut("reject")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RejectByCompany(
    CompanyRejectDto dto)
        {
            await _adminReviewService
                .RejectByCompanyAsync(dto);

            return Ok(new
            {
                Message = "Request Rejected Successfully"
            });
        }
    }
}
