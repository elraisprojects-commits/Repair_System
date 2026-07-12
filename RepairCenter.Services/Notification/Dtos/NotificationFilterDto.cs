using RepairCenter.data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Notification.Dtos
{

    public class NotificationFilterDto
    {
        public bool? IsRead { get; set; }

        public NotificationType? Type { get; set; }
    }
}