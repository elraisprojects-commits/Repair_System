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
        private readonly IEmployeeBonusService _service;

        public EmployeeBonusController(
            IEmployeeBonusService service)
        {
            _service = service;
        }


      
        // ADD BONUS + DEDUCTION
      

        [HttpPost]
        public async Task<IActionResult> Add(
            [FromBody] AddEmployeeBonusDto dto)
        {
            try
            {
                var adminId =
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(adminId))
                    return Unauthorized();


                var result =
                    await _service.AddAsync(
                        dto,
                        adminId);


                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }


        
        // GET ALL
       

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result =
                await _service.GetAllAsync();

            return Ok(result);
        }


   
        // GET EMPLOYEE
        

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult>
            GetEmployee(string employeeId)
        {
            try
            {
                var result =
                    await _service
                        .GetEmployeeBonusesAsync(
                            employeeId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }


      

        [HttpGet("filter")]
        public async Task<IActionResult>
            Filter([FromQuery] EmployeeBonusFilterDto filter)
        {
            var result =
                await _service.FilterAsync(filter);

            return Ok(result);
        }


     

        [HttpGet("summary")]
        public async Task<IActionResult>
            Summary([FromQuery] EmployeeBonusFilterDto filter)
        {
            var result =
                await _service.GetSummaryAsync(filter);

            return Ok(result);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult>
            Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);

                return Ok(new
                {
                    message =
                        "Transaction deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
        }
    }
}