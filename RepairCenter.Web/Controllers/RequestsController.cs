using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairCenter.Services.Requests;
using RepairCenter.Services.Requests.Dtos;
using System.Security.Claims;

namespace RepairCenter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestsController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> CreateRequest(
            CreateRequestDto dto)
        {
            var userId =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var result = await _requestService.CreateRequestAsync(
              dto,
              userId);

                 return Ok(new
            {
             result.RequestId,
             result.RequestNumber
            }); 
        }

        [HttpGet("test")]
        [Authorize(Roles = "Receptionist")]
        public IActionResult Test()
        {
            return Ok("Receptionist Access Granted");
        }





        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var requests = await _requestService.GetAllAsync();

            return Ok(requests);
        }



        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var request = await _requestService.GetByIdAsync(id);

            if (request == null)
                return NotFound();

            return Ok(request);
        }



        [HttpGet("filter")]
         [Authorize]
        public async Task<IActionResult> Filter(
    [FromQuery] RequestFilterDto filter)
        {
            var result =
                await _requestService.FilterAsync(filter);

            return Ok(result);
        }
    }

}
