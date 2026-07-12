using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepairCenter.data.Entities;
using RepairCenter.Services.Auth;
using RepairCenter.Services.Auth.Dtos;

namespace RepairCenter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
                return Unauthorized("Invalid Username");

            var result =
                await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
                return Unauthorized("Invalid Password");

            var token = await _tokenService.CreateTokenAsync(user);

            return Ok(new
            {
                Token = token
            });
        }
    }
}
