using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairCenter.Services.DeliverRequest;
using RepairCenter.Services.DeliverRequest.Dtos;
using System.Security.Claims;

namespace RepairCenter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(
            IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpPut]
        public async Task<IActionResult> Deliver(
            DeliverRequestDto dto)
        {
            var userId =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            await _deliveryService.DeliverAsync(
                dto,
                userId);

            return Ok(new
            {
                Message = "Device delivered successfully."
            });
        }
    }
}