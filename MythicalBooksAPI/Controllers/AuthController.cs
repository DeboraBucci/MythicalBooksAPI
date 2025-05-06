using Microsoft.AspNetCore.Mvc;
using MythicalBooksAPI.Dtos.Auth;
using MythicalBooksAPI.Helpers.Validators;
using MythicalBooksAPI.Interfaces.Services;

namespace MythicalBooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            ValidationResult result = await _authService.RegisterUserAsync(registerUserDto);

            if (!result.IsValid) return BadRequest(result.ErrorResponse);

            return Ok(new { message = "User registered successfully!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            AuthResponse? result = await _authService.LoginUserAsync(loginUserDto);

            if (result == null) return BadRequest("Couldn't log in, try again!");

            return Ok(result);
        }
    }
}
