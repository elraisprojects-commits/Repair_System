using AutoMapper;
using RepairCenter.data.Entities;
using RepairCenter.Services.EmployeeBonuses.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.EmployeeBonuses
{
   

    public class EmployeeBonusProfile : Profile
    {
        public EmployeeBonusProfile()
        {
            CreateMap<AddEmployeeBonusDto, EmployeeBonus>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore())

                .ForMember(dest => dest.Employee,
                    opt => opt.Ignore())

                .ForMember(dest => dest.CreatedBy,
                    opt => opt.Ignore())

                .ForMember(dest => dest.CreatedById,
                    opt => opt.Ignore())

                .ForMember(dest => dest.CreatedAt,
                    opt => opt.Ignore());

            CreateMap<EmployeeBonus, EmployeeBonusDto>()
                .ForMember(dest => dest.EmployeeName,
                    opt => opt.MapFrom(src => src.Employee.FullName))

                .ForMember(dest => dest.CreatedBy,
                    opt => opt.MapFrom(src => src.CreatedBy.FullName))

                .ForMember(dest => dest.NetAmount,
                    opt => opt.MapFrom(src =>
                        src.BonusAmount - src.DeductionAmount));
        }
    }
}