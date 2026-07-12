using AutoMapper;
using RepairCenter.data.Entities;
using RepairCenter.Services.Requests.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Requests
{

    public class RequestProfile : Profile
    {
        public RequestProfile()
        {
            CreateMap<CreateRequestDto, Customer>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.CustomerName));

            CreateMap<CreateRequestDto, Device>()
                .ForMember(
                    dest => dest.CustomerId,
                    opt => opt.Ignore());

            CreateMap<ServiceRequest, RequestListDto>()
                .ForMember(
                    dest => dest.RequestId,
                    opt => opt.MapFrom(src => src.Id))

                .ForMember(
                    dest => dest.CustomerName,
                    opt => opt.MapFrom(src => src.Customer.Name))

                .ForMember(
                    dest => dest.PhoneNumber,
                    opt => opt.MapFrom(src => src.Customer.PhoneNumber1))

                .ForMember(
                    dest => dest.DeviceType,
                    opt => opt.MapFrom(src => src.Device.DeviceType))

                .ForMember(
                    dest => dest.Brand,
                    opt => opt.MapFrom(src => src.Device.Brand))

                .ForMember(
                    dest => dest.Model,
                    opt => opt.MapFrom(src => src.Device.Model))

                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<ServiceRequest, RequestDetailsDto>()
                .ForMember(
                    dest => dest.RequestId,
                    opt => opt.MapFrom(src => src.Id))

                .ForMember(
                    dest => dest.CustomerName,
                    opt => opt.MapFrom(src => src.Customer.Name))

                .ForMember(
                    dest => dest.PhoneNumber1,
                    opt => opt.MapFrom(src => src.Customer.PhoneNumber1))

                .ForMember(
                    dest => dest.PhoneNumber2,
                    opt => opt.MapFrom(src => src.Customer.PhoneNumber2))

                .ForMember(
                    dest => dest.Area,
                    opt => opt.MapFrom(src => src.Customer.Area))

                .ForMember(
                    dest => dest.DeviceType,
                    opt => opt.MapFrom(src => src.Device.DeviceType))

                .ForMember(
                    dest => dest.Brand,
                    opt => opt.MapFrom(src => src.Device.Brand))

                .ForMember(
                    dest => dest.Model,
                    opt => opt.MapFrom(src => src.Device.Model))

                .ForMember(
                    dest => dest.SerialNumber,
                    opt => opt.MapFrom(src => src.Device.SerialNumber))

                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}

