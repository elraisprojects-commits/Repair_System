using AutoMapper;
using RepairCenter.data.Entities;
using RepairCenter.Services.EmployeeReports.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.EmployeeReports
{
    public class EmployeeReportProfile : Profile
    {
        public EmployeeReportProfile()
        {
            CreateMap<ApplicationUser, EmployeePerformanceDto>()

                .ForMember(dest => dest.EmployeeId,
                    opt => opt.MapFrom(src => src.Id))

                .ForMember(dest => dest.EmployeeName,
                    opt => opt.MapFrom(src => src.FullName))

                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.UserName))

                .ForMember(dest => dest.BranchName,
                    opt => opt.MapFrom(src =>
                        src.Branch != null
                            ? src.Branch.Name
                            : null))

                .ForMember(dest => dest.Salary,
                    opt => opt.MapFrom(src => src.Salary))

               
                .ForMember(dest => dest.Role,
                    opt => opt.Ignore())

                .ForMember(dest => dest.AssignedRequests,
                    opt => opt.Ignore())

                .ForMember(dest => dest.CompletedRequests,
                    opt => opt.Ignore())

                .ForMember(dest => dest.DeliveredRequests,
                    opt => opt.Ignore())

                .ForMember(dest => dest.CancelledRequests,
                    opt => opt.Ignore())

                .ForMember(dest => dest.TotalRevenue,
                    opt => opt.Ignore())

                .ForMember(dest => dest.TotalBonus,
                    opt => opt.Ignore());
        }
    }
}