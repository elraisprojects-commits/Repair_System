using AutoMapper;
using RepairCenter.data.Entities;
using RepairCenter.Services.RequestReports.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.RequestReports
{
    public class RequestReportProfile : Profile
    {
        public RequestReportProfile()
        {
            CreateMap<ServiceRequest, RequestReportDto>()

                .ForMember(dest => dest.RequestId,
                    opt => opt.MapFrom(src => src.Id))

                .ForMember(dest => dest.RequestNumber,
                    opt => opt.MapFrom(src => src.RequestNumber))

                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status))

                .ForMember(dest => dest.Cost,
                    opt => opt.MapFrom(src => src.Cost))

                .ForMember(dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => src.CreatedAt))

                .ForMember(dest => dest.CompletedAt,
                  opt => opt.MapFrom(src => src.CompletedAt))

                .ForMember(dest => dest.DeliveredAt,
                    opt => opt.MapFrom(src => src.DeliveredAt))

           
                .ForMember(dest => dest.CustomerName,
                    opt => opt.Ignore())

                .ForMember(dest => dest.Phone,
                    opt => opt.Ignore())

                .ForMember(dest => dest.Device,
                    opt => opt.Ignore())

                .ForMember(dest => dest.Branch,
                    opt => opt.Ignore())

                .ForMember(dest => dest.Receptionist,
                    opt => opt.Ignore())

                .ForMember(dest => dest.Specialist,
                    opt => opt.Ignore());
        }
    }
}