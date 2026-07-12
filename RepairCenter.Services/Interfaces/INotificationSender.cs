using RepairCenter.Services.Notification.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Interfaces
{
    public interface INotificationSender
    {
        Task SendAsync(
            string userId,
            NotificationDto notification);
    }
}