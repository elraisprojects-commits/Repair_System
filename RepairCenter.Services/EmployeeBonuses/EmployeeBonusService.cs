using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RepairCenter.data.Contexts;
using RepairCenter.data.Entities;
using RepairCenter.Services.EmployeeBonuses.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.EmployeeBonuses
{
    

    public class EmployeeBonusService : IEmployeeBonusService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeBonusService(
            AppDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

      
        // ADD BONUS + DEDUCTION
        

        public async Task<EmployeeBonusDto> AddAsync(
            AddEmployeeBonusDto dto,
            string adminId)
        {
            if (dto.BonusAmount < 0)
                throw new Exception("Bonus amount cannot be negative.");

            if (dto.DeductionAmount < 0)
                throw new Exception("Deduction amount cannot be negative.");

            if (dto.BonusAmount == 0 &&
                dto.DeductionAmount == 0)
            {
                throw new Exception(
                    "Bonus or deduction amount must be greater than zero.");
            }

            var employee = await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.Id == dto.EmployeeId);

            if (employee == null)
                throw new Exception("Employee not found.");

            var admin = await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.Id == adminId);

            if (admin == null)
                throw new Exception("Admin not found.");

            var transaction = _mapper.Map<EmployeeBonus>(dto);

            transaction.EmployeeId = dto.EmployeeId;

            transaction.CreatedById = adminId;

            transaction.CreatedAt = DateTime.UtcNow;

            _context.EmployeeBonuses.Add(transaction);

            await _context.SaveChangesAsync();

            await _context.Entry(transaction)
                .Reference(x => x.Employee)
                .LoadAsync();

            await _context.Entry(transaction)
                .Reference(x => x.CreatedBy)
                .LoadAsync();

            return _mapper.Map<EmployeeBonusDto>(transaction);
        }


        // =========================================================
        // GET ALL
        // =========================================================

        public async Task<List<EmployeeBonusDto>> GetAllAsync()
        {
            var transactions = await _context.EmployeeBonuses
                .Include(x => x.Employee)
                .Include(x => x.CreatedBy)

                .OrderByDescending(x => x.CreatedAt)

                .ToListAsync();

            return _mapper.Map<List<EmployeeBonusDto>>(
                transactions);
        }


        // =========================================================
        // GET EMPLOYEE TRANSACTIONS
        // =========================================================

        public async Task<List<EmployeeBonusDto>>
            GetEmployeeBonusesAsync(string employeeId)
        {
            var employeeExists =
                await _context.Users
                    .AnyAsync(x => x.Id == employeeId);

            if (!employeeExists)
                throw new Exception("Employee not found.");

            var transactions =
                await _context.EmployeeBonuses

                    .Include(x => x.Employee)

                    .Include(x => x.CreatedBy)

                    .Where(x =>
                        x.EmployeeId == employeeId)

                    .OrderByDescending(x =>
                        x.CreatedAt)

                    .ToListAsync();

            return _mapper.Map<List<EmployeeBonusDto>>(
                transactions);
        }


        
        // FILTER
       

        public async Task<List<EmployeeBonusDto>>
            FilterAsync(EmployeeBonusFilterDto filter)
        {
            var query = _context.EmployeeBonuses
                .Include(x => x.Employee)
                .Include(x => x.CreatedBy)
                .AsQueryable();


            // Employee
            if (!string.IsNullOrWhiteSpace(filter.EmployeeId))
            {
                query = query.Where(x =>
                    x.EmployeeId == filter.EmployeeId);
            }


            // Specific Date
            if (filter.Date.HasValue)
            {
                var date = filter.Date.Value.Date;

                var nextDate = date.AddDays(1);

                query = query.Where(x =>
                    x.CreatedAt >= date &&
                    x.CreatedAt < nextDate);
            }


            // Month
            if (filter.Month.HasValue)
            {
                var month = filter.Month.Value;

                query = query.Where(x =>
                    x.CreatedAt.Month == month);
            }


            // Year
            if (filter.Year.HasValue)
            {
                var year = filter.Year.Value;

                query = query.Where(x =>
                    x.CreatedAt.Year == year);
            }


            // From Date
            if (filter.FromDate.HasValue)
            {
                var fromDate =
                    filter.FromDate.Value.Date;

                query = query.Where(x =>
                    x.CreatedAt >= fromDate);
            }


            // To Date
            if (filter.ToDate.HasValue)
            {
                var toDate =
                    filter.ToDate.Value.Date
                    .AddDays(1);

                query = query.Where(x =>
                    x.CreatedAt < toDate);
            }


            var transactions = await query

                .OrderByDescending(x =>
                    x.CreatedAt)

                .ToListAsync();


            return _mapper.Map<List<EmployeeBonusDto>>(
                transactions);
        }


       
        // SUMMARY
       

        public async Task<EmployeeBonusSummaryDto>
            GetSummaryAsync(EmployeeBonusFilterDto filter)
        {
            var query = _context.EmployeeBonuses
                .AsQueryable();


            // Employee
            if (!string.IsNullOrWhiteSpace(filter.EmployeeId))
            {
                query = query.Where(x =>
                    x.EmployeeId == filter.EmployeeId);
            }


            // Specific Date
            if (filter.Date.HasValue)
            {
                var date = filter.Date.Value.Date;

                var nextDate = date.AddDays(1);

                query = query.Where(x =>
                    x.CreatedAt >= date &&
                    x.CreatedAt < nextDate);
            }


            // Month
            if (filter.Month.HasValue)
            {
                var month = filter.Month.Value;

                query = query.Where(x =>
                    x.CreatedAt.Month == month);
            }


            // Year
            if (filter.Year.HasValue)
            {
                var year = filter.Year.Value;

                query = query.Where(x =>
                    x.CreatedAt.Year == year);
            }


            // From Date
            if (filter.FromDate.HasValue)
            {
                var fromDate =
                    filter.FromDate.Value.Date;

                query = query.Where(x =>
                    x.CreatedAt >= fromDate);
            }


            // To Date
            if (filter.ToDate.HasValue)
            {
                var toDate =
                    filter.ToDate.Value.Date
                    .AddDays(1);

                query = query.Where(x =>
                    x.CreatedAt < toDate);
            }


            var totalBonus =
                await query.SumAsync(x =>
                    (decimal?)x.BonusAmount) ?? 0;


            var totalDeduction =
                await query.SumAsync(x =>
                    (decimal?)x.DeductionAmount) ?? 0;


            var count =
                await query.CountAsync();


            return new EmployeeBonusSummaryDto
            {
                TotalBonus = totalBonus,

                TotalDeduction = totalDeduction,

                NetAmount =
                    totalBonus - totalDeduction,

                TransactionsCount = count
            };
        }


       
        // DELETE
     

        public async Task DeleteAsync(int id)
        {
            var transaction =
                await _context.EmployeeBonuses
                    .FirstOrDefaultAsync(x =>
                        x.Id == id);

            if (transaction == null)
                throw new Exception(
                    "Bonus/Deduction transaction not found.");


            _context.EmployeeBonuses
                .Remove(transaction);

            await _context.SaveChangesAsync();
        }
    }
}

