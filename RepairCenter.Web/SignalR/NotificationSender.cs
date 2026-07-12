using Microsoft.AspNetCore.SignalR;
using RepairCenter.Services.Interfaces;
using RepairCenter.Services.Notification.Dtos;
using RepairCenter.Web.Hubs;

namespace RepairCenter.Web.SignalR
{
    public class NotificationSender : INotificationSender
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationSender(
            IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendAsync(
            string userId,
            NotificationDto notification)
        {
            await _hubContext
                .Clients
                .User(userId)
                .SendAsync(
                    "ReceiveNotification",
                    notification);
        }
    }
}