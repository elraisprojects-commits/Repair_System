using AutoMapper;
using RepairCenter.Services.Notification.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotificationEntity =  RepairCenter.data.Entities.Notification;
namespace RepairCenter.Services.Notification
{
    
        public class NotificationProfile : Profile
        {
            public NotificationProfile()
            {
                CreateMap<NotificationEntity, NotificationDto>();

                CreateMap<CreateNotificationDto, NotificationEntity>() ;
            }
        }
    }