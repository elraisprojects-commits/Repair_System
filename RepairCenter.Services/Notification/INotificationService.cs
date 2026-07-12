using RepairCenter.data.Enums;
using RepairCenter.Services.Notification.Dtos;
using RepairCenter.Services.Notification.Dtos.RepairCenter.Services.Notifications.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Notification
{

    public interface INotificationService
    {
        
        Task CreateAsync(CreateNotificationDto dto);

      
        Task<List<NotificationDto>> GetMyNotificationsAsync(
            string userId,
            NotificationFilterDto? filter);

       
        Task<NotificationCountDto> GetUnreadCountAsync(
            string userId);

       
        Task MarkAsReadAsync(
            int notificationId,
            string userId);

       
        Task MarkAllAsReadAsync(
            string userId);

       
        Task DeleteAsync(
            int notificationId,
            string userId);


        Task CreateForAllEmployeesAsync(
        NotificationType type,
        int requestId,
        string requestNumber,
        bool includeReceptionist = false);
    }
}
