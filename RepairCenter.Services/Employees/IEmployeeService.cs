using RepairCenter.Services.Employees.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Employees
{
    public interface IEmployeeService
    {
        // Create
        Task CreateAsync(CreateEmployeeDto dto);

        // Read
        Task<List<EmployeeListDto>> GetAllAsync();

        Task<EmployeeDetailsDto?> GetByIdAsync(string id);

        // Update
        Task UpdateAsync(UpdateEmployeeDto dto);

        // Active / Inactive
        Task ActivateAsync(string id);

        Task DeactivateAsync(string id);

        // Role
        Task ChangeRoleAsync(ChangeRoleDto dto);

        // Password
        Task ResetPasswordAsync(ResetPasswordDto dto);



        // Employee Profile
        Task<EmployeeProfileDto> GetProfileAsync(string id);

        Task<List<EmployeeListDto>> FilterAsync(
          
            EmployeeFilterDto filter);


        Task DeleteAsync(string id);
    }
}
