using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairCenter.Services.RequestNotes;
using RepairCenter.Services.RequestNotes.Dtos;
using System.Security.Claims;

namespace RepairCenter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Specialist")]
    public class RequestNotesController : ControllerBase
    {
        private readonly IRequestNoteService _requestNoteService;

        public RequestNotesController(
            IRequestNoteService requestNoteService)
        {
            _requestNoteService = requestNoteService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(
            AddRequestNoteDto dto)
        {
            var userId =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            await _requestNoteService.AddNoteAsync(
                dto,
                userId);

            return Ok(new
            {
                Message = "Request updated successfully."
            });
        }
    }
}
