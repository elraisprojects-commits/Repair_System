using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairCenter.Services.Notification;
using RepairCenter.Services.Notification.Dtos;
using System.Security.Claims;

namespace RepairCenter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(
            INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyNotifications(
            [FromQuery] NotificationFilterDto? filter)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _notificationService
                .GetMyNotificationsAsync(userId!, filter);

            return Ok(result);
        }

        [HttpGet("unread-count")]
        public async Task<IActionResult> GetUnreadCount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _notificationService
                .GetUnreadCountAsync(userId!);

            return Ok(result);
        }

        [HttpPut("{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _notificationService
                .MarkAsReadAsync(id, userId!);

            return Ok(new
            {
                Message = "Notification marked as read."
            });
        }

        [HttpPut("read-all")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _notificationService
                .MarkAllAsReadAsync(userId!);

            return Ok(new
            {
                Message = "All notifications marked as read."
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _notificationService
                .DeleteAsync(id, userId!);

            return Ok(new
            {
                Message = "Notification deleted successfully."
            });
        }
    }
}