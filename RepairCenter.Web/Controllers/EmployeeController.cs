using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairCenter.Services.Employees;
using RepairCenter.Services.Employees.Dtos;
using System.Security.Claims;

namespace RepairCenter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(
            IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        #region Create

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            CreateEmployeeDto dto)
        {
            await _employeeService.CreateAsync(dto);

            return Ok(new
            {
                Message = "Employee created successfully."
            });
        }

        #endregion



        #region Get All

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _employeeService.GetAllAsync();

            return Ok(result);
        }

        #endregion



        #region Get By Id

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _employeeService.GetByIdAsync(id);

            return Ok(result);
        }

        #endregion



        #region Employee Profile (Admin)

        [HttpGet("profile/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetProfile(string id)
        {
            var result = await _employeeService.GetProfileAsync(id);

            return Ok(result);
        }

        #endregion



        #region My Profile

        [HttpGet("my-profile")]
        [Authorize(Roles = "Admin,Receptionist,Specialist")]
        public async Task<IActionResult> MyProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _employeeService.GetProfileAsync(userId);

            return Ok(result);
        }

        #endregion



        #region Update

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(
            UpdateEmployeeDto dto)
        {
            await _employeeService.UpdateAsync(dto);

            return Ok(new
            {
                Message = "Employee updated successfully."
            });
        }

        #endregion



        #region Activate

        [HttpPut("activate/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Activate(string id)
        {
            await _employeeService.ActivateAsync(id);

            return Ok(new
            {
                Message = "Employee activated successfully."
            });
        }

        #endregion



        #region Deactivate

        [HttpPut("deactivate/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Deactivate(string id)
        {
            await _employeeService.DeactivateAsync(id);

            return Ok(new
            {
                Message = "Employee deactivated successfully."
            });
        }

        #endregion



        #region Change Role

        [HttpPut("change-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole(
            ChangeRoleDto dto)
        {
            await _employeeService.ChangeRoleAsync(dto);

            return Ok(new
            {
                Message = "Role changed successfully."
            });
        }

        #endregion



        #region Reset Password

        [HttpPut("reset-password")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ResetPassword(
            ResetPasswordDto dto)
        {
            await _employeeService.ResetPasswordAsync(dto);

            return Ok(new
            {
                Message = "Password reset successfully."
            });
        }

        #endregion


        #region Filter

        [HttpPost("filter")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Filter(
            EmployeeFilterDto filter)
        {
            var result = await _employeeService.FilterAsync(filter);

            return Ok(result);
        }

        #endregion
    }
}