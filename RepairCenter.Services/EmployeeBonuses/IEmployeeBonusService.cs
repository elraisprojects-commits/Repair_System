using RepairCenter.Services.EmployeeBonuses.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.EmployeeBonuses
{
    public interface IEmployeeBonusService
    {
        // Add Bonus
        Task AddBonusAsync(
            AddEmployeeBonusDto dto,
            string adminId);

        // Get All Bonuses
        Task<List<EmployeeBonusDto>> GetAllAsync();

        // Get Employee Bonuses
        Task<List<EmployeeBonusDto>> GetEmployeeBonusesAsync(
            string employeeId);

        // Filter
        Task<List<EmployeeBonusDto>> FilterAsync(
            EmployeeBonusFilterDto filter);

        // Delete Bonus
        Task DeleteAsync(int id);
    }
}
