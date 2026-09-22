using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepairCenter.data.Contexts;
using RepairCenter.data.Entities;
using RepairCenter.data.Enums;
using RepairCenter.Services.EmployeeReports.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.EmployeeReports
{
    public class EmployeeReportService : IEmployeeReportService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public EmployeeReportService(
            AppDbContext context,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<EmployeeReportResponseDto> GetReportAsync(
            EmployeeReportFilterDto dto)
        {
            var employeesQuery = _context.Users
                .Include(x => x.Branch)
                .AsQueryable();

            // ==========================
            // Employee Name
            // ==========================

            if (!string.IsNullOrWhiteSpace(dto.EmployeeName))
            {
                employeesQuery = employeesQuery.Where(x =>
                    x.FullName!.Contains(dto.EmployeeName));
            }

            // ==========================
            // Branch
            // ==========================

            if (dto.BranchId.HasValue)
            {
                employeesQuery = employeesQuery.Where(x =>
                    x.BranchId == dto.BranchId.Value);
            }

            var employees =
                await employeesQuery.ToListAsync();

            var response =
                new EmployeeReportResponseDto();

            foreach (var employee in employees)
            {
                var employeeDto =
                    _mapper.Map<EmployeePerformanceDto>(employee);

                // ==========================
                // Role
                // ==========================

                var roles =
                    await _userManager.GetRolesAsync(employee);

                employeeDto.Role =
                    roles.FirstOrDefault() ?? "";

                // ==========================
                // Role Filter
                // ==========================

                if (!string.IsNullOrWhiteSpace(dto.Role))
                {
                    if (!employeeDto.Role.Equals(
                        dto.Role,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                }

                // ==========================
                // Requests Query
                // ==========================

                var requests = _context.ServiceRequests
                    .Where(x =>
                        x.SpecialistId == employee.Id)
                    .AsQueryable();

                if (dto.FromDate.HasValue)
                {
                    var fromDate =
                        dto.FromDate.Value.Date;

                    requests = requests.Where(x =>
                        x.CreatedAt >= fromDate);
                }

                if (dto.ToDate.HasValue)
                {
                    var toDate =
                        dto.ToDate.Value.Date.AddDays(1);

                    requests = requests.Where(x =>
                        x.CreatedAt < toDate);
                }

                // ==========================
                // Requests Statistics
                // ==========================

                employeeDto.AssignedRequests =
                    await requests.CountAsync();

                employeeDto.CompletedRequests =
                    await requests.CountAsync(x =>
                        x.Status == RequestStatus.Completed);

                employeeDto.DeliveredRequests =
                    await requests.CountAsync(x =>
                        x.Status == RequestStatus.Delivered);

                employeeDto.CancelledRequests =
                    await requests.CountAsync(x =>
                        x.Status == RequestStatus.CompanyRejected ||
                        x.Status == RequestStatus.CancelledByCustomer);

                // ==========================
                // Revenue
                // ==========================

                employeeDto.TotalRevenue =
                    await requests
                        .Where(x =>
                            x.Status == RequestStatus.Delivered)
                        .SumAsync(x =>
                            x.Cost ?? 0);

                // ==========================
                // Bonus + Deduction Query
                // ==========================

                var transactions =
                    _context.EmployeeBonuses
                        .Where(x =>
                            x.EmployeeId == employee.Id)
                        .AsQueryable();

                // ==========================
                // Transaction From Date
                // ==========================

                if (dto.FromDate.HasValue)
                {
                    var fromDate =
                        dto.FromDate.Value.Date;

                    transactions =
                        transactions.Where(x =>
                            x.CreatedAt >= fromDate);
                }

                // ==========================
                // Transaction To Date
                // ==========================

                if (dto.ToDate.HasValue)
                {
                    var toDate =
                        dto.ToDate.Value.Date.AddDays(1);

                    transactions =
                        transactions.Where(x =>
                            x.CreatedAt < toDate);
                }

                // ==========================
                // Total Bonus
                // ==========================

                employeeDto.TotalBonus =
                    await transactions
                        .SumAsync(x =>
                            (decimal?)x.BonusAmount) ?? 0;

                // ==========================
                // Total Deduction
                // ==========================

                employeeDto.TotalDeduction =
                    await transactions
                        .SumAsync(x =>
                            (decimal?)x.DeductionAmount) ?? 0;

                // ==========================
                // Net Salary
                // ==========================

                employeeDto.NetSalary =
                    employeeDto.BasicSalary
                    + employeeDto.TotalBonus
                    - employeeDto.TotalDeduction;

                // ==========================
                // Add Employee
                // ==========================

                response.Employees.Add(employeeDto);
            }

            // ==========================
            // Summary
            // ==========================

            response.TotalEmployees =
                response.Employees.Count;

            response.TotalAssignedRequests =
                response.Employees.Sum(x =>
                    x.AssignedRequests);

            response.TotalCompletedRequests =
                response.Employees.Sum(x =>
                    x.CompletedRequests);

            response.TotalDeliveredRequests =
                response.Employees.Sum(x =>
                    x.DeliveredRequests);

            response.TotalRevenue =
                response.Employees.Sum(x =>
                    x.TotalRevenue);

            return response;
        }
    }
}