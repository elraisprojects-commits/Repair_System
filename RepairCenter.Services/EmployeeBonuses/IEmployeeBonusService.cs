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
        Task<EmployeeBonusDto> AddAsync(
            AddEmployeeBonusDto dto,
            string adminId);

        Task<List<EmployeeBonusDto>> GetAllAsync();

        Task<List<EmployeeBonusDto>> GetEmployeeBonusesAsync(
            string employeeId);

        Task<List<EmployeeBonusDto>> FilterAsync(
            EmployeeBonusFilterDto filter);

        Task<EmployeeBonusSummaryDto> GetSummaryAsync(
            EmployeeBonusFilterDto filter);

        Task DeleteAsync(int id);
    }
}
