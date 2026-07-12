using AutoMapper;
using RepairCenter.data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Invoices.Dtos
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<ServiceRequest, InvoiceDto>()

                .ForMember(dest => dest.RequestNumber,
                    opt => opt.MapFrom(src => src.RequestNumber))

                .ForMember(dest => dest.RequestDate,
                    opt => opt.MapFrom(src => src.CreatedAt))

                // Customer

                .ForMember(dest => dest.CustomerName,
                    opt => opt.MapFrom(src => src.Customer.Name))

                .ForMember(dest => dest.PhoneNumber,
                    opt => opt.MapFrom(src => src.Customer.PhoneNumber1))

                .ForMember(dest => dest.Area,
                    opt => opt.MapFrom(src => src.Customer.Area))

                // Device

                .ForMember(dest => dest.DeviceType,
                    opt => opt.MapFrom(src => src.Device.DeviceType))

                .ForMember(dest => dest.Brand,
                    opt => opt.MapFrom(src => src.Device.Brand))

                .ForMember(dest => dest.Model,
                    opt => opt.MapFrom(src => src.Device.Model))

                .ForMember(dest => dest.SerialNumber,
                    opt => opt.MapFrom(src => src.Device.SerialNumber))

                // Request

                .ForMember(dest => dest.CustomerComplaint,
                    opt => opt.MapFrom(src => src.CustomerComplaint))

                .ForMember(dest => dest.ProblemCause,
                    opt => opt.MapFrom(src => src.ProblemCause))

                .ForMember(dest => dest.Cost,
                    opt => opt.MapFrom(src => src.Cost))

                .ForMember(dest => dest.ExpectedDays,
                    opt => opt.MapFrom(src => src.ExpectedDays))

                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status))

                // Branch

                .ForMember(dest => dest.BranchName,
                    opt => opt.MapFrom(src => src.Branch.Name))

                // Receptionist

                .ForMember(dest => dest.ReceptionistName,
                    opt => opt.MapFrom(src => src.ReceptionistName))

                // Specialist

                .ForMember(dest => dest.SpecialistName,
                    opt => opt.MapFrom(src =>
                        src.Specialist == null
                            ? null
                            : src.Specialist.FullName))

                // Delivery

                .ForMember(dest => dest.DeliveredByName,
                    opt => opt.MapFrom(src => src.DeliveredByName))

                .ForMember(dest => dest.DeliveredAt,
                    opt => opt.MapFrom(src => src.DeliveredAt));
        }
    }
}