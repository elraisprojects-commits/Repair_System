using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepairCenter.data.Contexts;
using RepairCenter.data.Entities;
using RepairCenter.data.Enums;
using RepairCenter.Services.Employees.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public EmployeeService(
      AppDbContext context,
      UserManager<ApplicationUser> userManager,
      IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        #region Create

        public async Task CreateAsync(CreateEmployeeDto dto)
        {
            var userNameExists = await _userManager
                .FindByNameAsync(dto.UserName);

            if (userNameExists != null)
                throw new Exception("Username already exists.");

            var nationalIdExists = await _userManager.Users
                .AnyAsync(x => x.NationalId == dto.NationalId);

            if (nationalIdExists)
                throw new Exception("National ID already exists.");

            var phoneExists = await _userManager.Users
                .AnyAsync(x => x.PhoneNumber == dto.PhoneNumber);

            if (phoneExists)
                throw new Exception("Phone Number already exists.");

            var employee = _mapper.Map<ApplicationUser>(dto);

            employee.IsActive = true;

            var result = await _userManager.CreateAsync(
                employee,
                dto.Password);

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(Environment.NewLine,
                        result.Errors.Select(x => x.Description)));
            }

            var roleResult = await _userManager
                .AddToRoleAsync(employee, dto.Role);

            if (!roleResult.Succeeded)
            {
                throw new Exception(
                    string.Join(Environment.NewLine,
                        roleResult.Errors.Select(x => x.Description)));
            }
        }

        #endregion

        #region Get All

        public async Task<List<EmployeeListDto>> GetAllAsync()
        {
            var employees = await _userManager.Users
                .Include(x => x.Branch)
                .OrderBy(x => x.FullName)
                .ToListAsync();

            var result = new List<EmployeeListDto>();

            foreach (var employee in employees)
            {
                var dto = _mapper.Map<EmployeeListDto>(employee);

                var roles = await _userManager
                    .GetRolesAsync(employee);

                dto.Role = roles.FirstOrDefault() ?? "";

                result.Add(dto);
            }

            return result;
        }

        #endregion

        #region Get By Id

        public async Task<EmployeeDetailsDto?> GetByIdAsync(string id)
        {
            var employee = await _userManager.Users
                .Include(x => x.Branch)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
                return null;

            var dto = _mapper.Map<EmployeeDetailsDto>(employee);

            var roles = await _userManager
                .GetRolesAsync(employee);

            dto.Role = roles.FirstOrDefault() ?? "";

            return dto;
        }

        #endregion

        #region Update

        public async Task UpdateAsync(UpdateEmployeeDto dto)
        {
            var employee = await _userManager
                .FindByIdAsync(dto.Id);

            if (employee == null)
                throw new Exception("Employee not found.");

            var phoneExists = await _userManager.Users
                .AnyAsync(x =>
                    x.PhoneNumber == dto.PhoneNumber &&
                    x.Id != dto.Id);

            if (phoneExists)
                throw new Exception("Phone Number already exists.");

            var nationalIdExists = await _userManager.Users
                .AnyAsync(x =>
                    x.NationalId == dto.NationalId &&
                    x.Id != dto.Id);

            if (nationalIdExists)
                throw new Exception("National ID already exists.");

            _mapper.Map(dto, employee);

            var result = await _userManager.UpdateAsync(employee);

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(Environment.NewLine,
                        result.Errors.Select(x => x.Description)));
            }
        }

        #endregion

        #region Activate

        public async Task ActivateAsync(string id)
        {
            var employee = await _userManager
                .FindByIdAsync(id);

            if (employee == null)
                throw new Exception("Employee not found.");

            employee.IsActive = true;

            var result = await _userManager.UpdateAsync(employee);

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(Environment.NewLine,
                        result.Errors.Select(x => x.Description)));
            }
        }

        #endregion

        #region Deactivate

        public async Task DeactivateAsync(string id)
        {
            var employee = await _userManager
                .FindByIdAsync(id);

            if (employee == null)
                throw new Exception("Employee not found.");



            employee.IsActive = false;

            var result = await _userManager.UpdateAsync(employee);

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(Environment.NewLine,
                        result.Errors.Select(x => x.Description)));
            }
        }

        #endregion

        #region Change Role

        public async Task ChangeRoleAsync(ChangeRoleDto dto)
        {
            var employee = await _userManager
                .FindByIdAsync(dto.UserId);

            if (employee == null)
                throw new Exception("Employee not found.");

            var currentRoles = await _userManager
                .GetRolesAsync(employee);

            if (currentRoles.Any())
            {
                var removeResult = await _userManager
                    .RemoveFromRolesAsync(employee, currentRoles);

                if (!removeResult.Succeeded)
                {
                    throw new Exception(
                        string.Join(Environment.NewLine,
                            removeResult.Errors.Select(x => x.Description)));
                }
            }

            var addResult = await _userManager
                .AddToRoleAsync(employee, dto.Role);

            if (!addResult.Succeeded)
            {
                throw new Exception(
                    string.Join(Environment.NewLine,
                        addResult.Errors.Select(x => x.Description)));
            }
        }

        #endregion

        #region Reset Password

        public async Task ResetPasswordAsync(
            ResetPasswordDto dto)
        {
            var employee = await _userManager
                .FindByIdAsync(dto.UserId);

            if (employee == null)
                throw new Exception("Employee not found.");

            var token = await _userManager
                .GeneratePasswordResetTokenAsync(employee);

            var result = await _userManager
                .ResetPasswordAsync(
                    employee,
                    token,
                    dto.NewPassword);

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(Environment.NewLine,
                        result.Errors.Select(x => x.Description)));
            }
        }

        #endregion

        #region Employee Profile

        public async Task<EmployeeProfileDto> GetProfileAsync(string id)
        {
            var employee = await _userManager.Users

                .Include(x => x.Branch)

                .FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
                throw new Exception("Employee not found.");

            var roles = await _userManager.GetRolesAsync(employee);

            var currentMonth = DateTime.UtcNow.Month;
            var currentYear = DateTime.UtcNow.Year;

            var currentMonthBonus = await _context.EmployeeBonuses

                .Where(x =>
                    x.EmployeeId == id )
                 

                .SumAsync(x => (decimal?)x.BonusAmount) ?? 0;

            var totalBonus = await _context.EmployeeBonuses

                .Where(x => x.EmployeeId == id)

                .SumAsync(x => (decimal?)x.BonusAmount) ?? 0;

            var lastBonus = await _context.EmployeeBonuses

                .Where(x => x.EmployeeId == id)

                .OrderByDescending(x => x.CreatedAt)

                .FirstOrDefaultAsync();

            var currentRequests = await _context.ServiceRequests

                .Where(x =>
                    x.SpecialistId == id &&
                    x.Status == RequestStatus.InProgress)

                .CountAsync();

            var completedRequests = await _context.ServiceRequests

                .Where(x =>
                    x.SpecialistId == id &&
                    x.Status == RequestStatus.Completed)

                .CountAsync();

            var deliveredRequests = await _context.ServiceRequests

                .Where(x =>
                    x.SpecialistId == id &&
                    x.Status == RequestStatus.Delivered)

                .CountAsync();

            var cancelledRequests = await _context.ServiceRequests

                .Where(x =>
                    x.SpecialistId == id &&
                    x.Status == RequestStatus.CancelledByCustomer)

                .CountAsync();

            var jobs = await _context.ServiceRequests

                .Include(x => x.Customer)

                .Include(x => x.Device)

                .Where(x =>
                    x.SpecialistId == id &&
                    x.Status == RequestStatus.InProgress)

                .Select(x => new EmployeeCurrentRequestDto
                {
                    RequestId = x.Id,

                    RequestNumber = x.RequestNumber,

                    CustomerName = x.Customer.Name,

                    Device = x.Device.Brand + " " + x.Device.Model,

                    Status = x.Status
                })

                .ToListAsync();

            return new EmployeeProfileDto
            {
                Id = employee.Id,

                FullName = employee.FullName,

                UserName = employee.UserName,

                PhoneNumber = employee.PhoneNumber,

                NationalId = employee.NationalId,

                Role = roles.FirstOrDefault() ?? "",

                BranchName = employee.Branch?.Name,

                Salary = employee.Salary,

                IsActive = employee.IsActive,

                CurrentMonthBonus = currentMonthBonus,

                TotalBonus = totalBonus,

                CurrentRequests = currentRequests,

                CompletedRequests = completedRequests,

                DeliveredRequests = deliveredRequests,

                CancelledRequests = cancelledRequests,

                LastBonusDate = lastBonus?.CreatedAt,

                CurrentJobs = jobs
            };
        }

        #endregion

        #region Delete

        public async Task DeleteAsync(string id)
        {
            var employee = await _userManager
                .FindByIdAsync(id);

            if (employee == null)
                throw new Exception("Employee not found.");

            var result = await _userManager
                .DeleteAsync(employee);

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(Environment.NewLine,
                        result.Errors.Select(x => x.Description)));
            }
        }

        #endregion
        public async Task<List<EmployeeListDto>> FilterAsync(
           EmployeeFilterDto filter)
        {
            var users = await _userManager.Users

                .Include(x => x.Branch)

                .ToListAsync();

            if (!string.IsNullOrWhiteSpace(filter.FullName))
            {
                users = users
                    .Where(x =>
                        x.FullName != null &&
                        x.FullName.Contains(filter.FullName))
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(filter.Role))
            {
                var filteredUsers = new List<ApplicationUser>();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Any(x => x == filter.Role))
                    {
                        filteredUsers.Add(user);
                    }
                }

                users = filteredUsers;
            }

            var result = new List<EmployeeListDto>();

            foreach (var user in users)
            {
                var dto = _mapper.Map<EmployeeListDto>(user);

                dto.Role = (await _userManager.GetRolesAsync(user))
                    .FirstOrDefault();

                result.Add(dto);
            }

            return result;
        }
    }
}
