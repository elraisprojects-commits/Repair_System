using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairCenter.Services.EmployeeBonuses;
using RepairCenter.Services.EmployeeBonuses.Dtos;
using System.Security.Claims;

namespace RepairCenter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class EmployeeBonusController : ControllerBase
    {
        private readonly IEmployeeBonusService _employeeBonusService;

        public EmployeeBonusController(
            IEmployeeBonusService employeeBonusService)
        {
            _employeeBonusService = employeeBonusService;
        }

        #region Add Bonus

        [HttpPost]
        public async Task<IActionResult> AddBonus(
            AddEmployeeBonusDto dto)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _employeeBonusService.AddBonusAsync(
                dto,
                adminId!);

            return Ok(new
            {
                Message = "Bonus added successfully."
            });
        }

        #endregion



        #region Get All

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _employeeBonusService.GetAllAsync();

            return Ok(result);
        }

        #endregion



        #region Get Employee Bonuses

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetEmployeeBonuses(
            string employeeId)
        {
            var result = await _employeeBonusService
                .GetEmployeeBonusesAsync(employeeId);

            return Ok(result);
        }

        #endregion



        #region Filter

        [HttpPost("filter")]
        public async Task<IActionResult> Filter(
            EmployeeBonusFilterDto filter)
        {
            var result = await _employeeBonusService
                .FilterAsync(filter);

            return Ok(result);
        }

        #endregion



        #region Delete

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeBonusService.DeleteAsync(id);

            return Ok(new
            {
                Message = "Bonus deleted successfully."
            });
        }

        #endregion
    }
}