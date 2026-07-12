using AutoMapper;
using RepairCenter.data.Entities;
using RepairCenter.Services.Employees.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Employees
{
   
        public class EmployeeProfile : Profile
        {
            public EmployeeProfile()
            {
                // Create Employee
                CreateMap<CreateEmployeeDto, ApplicationUser>()
                    .ForMember(dest => dest.Id,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.PasswordHash,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.SecurityStamp,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.ConcurrencyStamp,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.Email,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.NormalizedEmail,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.NormalizedUserName,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.EmailConfirmed,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.PhoneNumberConfirmed,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.LockoutEnabled,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.LockoutEnd,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.AccessFailedCount,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.TwoFactorEnabled,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.Branch,
                        opt => opt.Ignore());



                // Update Employee
                CreateMap<UpdateEmployeeDto, ApplicationUser>()

                    .ForMember(dest => dest.Id,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.UserName,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.PasswordHash,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.SecurityStamp,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.ConcurrencyStamp,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.Email,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.NormalizedEmail,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.NormalizedUserName,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.EmailConfirmed,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.PhoneNumberConfirmed,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.LockoutEnabled,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.LockoutEnd,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.AccessFailedCount,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.TwoFactorEnabled,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.Branch,
                        opt => opt.Ignore());



                // List
                CreateMap<ApplicationUser, EmployeeListDto>()
                    .ForMember(dest => dest.Role,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.BranchName,
                        opt => opt.MapFrom(src =>
                            src.Branch != null
                                ? src.Branch.Name
                                : null));



                // Details
                CreateMap<ApplicationUser, EmployeeDetailsDto>()
                    .ForMember(dest => dest.Role,
                        opt => opt.Ignore())

                    .ForMember(dest => dest.BranchName,
                        opt => opt.MapFrom(src =>
                            src.Branch != null
                                ? src.Branch.Name
                                : null));
            }
        }
}

