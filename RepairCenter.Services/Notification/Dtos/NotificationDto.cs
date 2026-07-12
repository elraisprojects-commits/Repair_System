using RepairCenter.data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Notification.Dtos
{
    public class NotificationDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Message { get; set; } = null!;

        public NotificationType Type { get; set; }

        public bool IsRead { get; set; }

        public int? RequestId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
