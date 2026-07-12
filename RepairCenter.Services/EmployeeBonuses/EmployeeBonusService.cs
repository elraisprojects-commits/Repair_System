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

        #region Add Bonus

        public async Task AddBonusAsync(
            AddEmployeeBonusDto dto,
            string adminId)
        {
            var employee = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == dto.EmployeeId);

            if (employee == null)
                throw new Exception("Employee not found.");

            var month = DateTime.UtcNow.Month;
            var year = DateTime.UtcNow.Year;

            var exists = await _context.EmployeeBonuses
                .AnyAsync(x =>
                    x.EmployeeId == dto.EmployeeId &&
                    x.Month == month &&
                    x.Year == year);

            if (exists)
                throw new Exception("Bonus already added for this month.");

            var bonus = _mapper.Map<EmployeeBonus>(dto);

            bonus.Month = month;
            bonus.Year = year;
            bonus.CreatedById = adminId;

            _context.EmployeeBonuses.Add(bonus);

            await _context.SaveChangesAsync();
        }

        #endregion



        #region Get All

        public async Task<List<EmployeeBonusDto>> GetAllAsync()
        {
            var bonuses = await _context.EmployeeBonuses

                .Include(x => x.Employee)

                .Include(x => x.CreatedBy)

                .OrderByDescending(x => x.Year)

                .ThenByDescending(x => x.Month)

                .ThenByDescending(x => x.CreatedAt)

                .ToListAsync();

            return _mapper.Map<List<EmployeeBonusDto>>(bonuses);
        }

        #endregion
        #region Get Employee Bonuses

        public async Task<List<EmployeeBonusDto>>
            GetEmployeeBonusesAsync(string employeeId)
        {
            var employeeExists = await _context.Users
                .AnyAsync(x => x.Id == employeeId);

            if (!employeeExists)
                throw new Exception("Employee not found.");

            var bonuses = await _context.EmployeeBonuses

                .Include(x => x.Employee)

                .Include(x => x.CreatedBy)

                .Where(x => x.EmployeeId == employeeId)

                .OrderByDescending(x => x.Year)

                .ThenByDescending(x => x.Month)

                .ThenByDescending(x => x.CreatedAt)

                .ToListAsync();

            return _mapper.Map<List<EmployeeBonusDto>>(bonuses);
        }

        #endregion



        #region Filter

        public async Task<List<EmployeeBonusDto>>
            FilterAsync(EmployeeBonusFilterDto filter)
        {
            var query = _context.EmployeeBonuses

                .Include(x => x.Employee)

                .Include(x => x.CreatedBy)

                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.EmployeeId))
            {
                query = query.Where(x =>
                    x.EmployeeId == filter.EmployeeId);
            }

            if (filter.Month.HasValue)
            {
                query = query.Where(x =>
                    x.Month == filter.Month.Value);
            }

            if (filter.Year.HasValue)
            {
                query = query.Where(x =>
                    x.Year == filter.Year.Value);
            }

            var bonuses = await query

                .OrderByDescending(x => x.Year)

                .ThenByDescending(x => x.Month)

                .ThenByDescending(x => x.CreatedAt)

                .ToListAsync();

            return _mapper.Map<List<EmployeeBonusDto>>(bonuses);
        }

        #endregion



        #region Delete

        public async Task DeleteAsync(int id)
        {
            var bonus = await _context.EmployeeBonuses
                .FirstOrDefaultAsync(x => x.Id == id);

            if (bonus == null)
                throw new Exception("Bonus not found.");

            _context.EmployeeBonuses.Remove(bonus);

            await _context.SaveChangesAsync();
        }
        #endregion
    }
}

