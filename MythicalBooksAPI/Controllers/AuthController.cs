using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MythicalBooksAPI.Models.Auth;
using MythicalBooksAPI.Mappers;
using MythicalBooksAPI.Helpers;
using System.Text.RegularExpressions;
using MythicalBooksAPI.Data.Contexts;
using MythicalBooksAPI.Dtos.Auth;
using MythicalBooksAPI.Helpers.Validators;
using MythicalBooksAPI.Services;

namespace MythicalBooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly AuthContext _context;
        private readonly TokenHelper _tokenHelper;

        public AuthController(AuthService authService, AuthContext context, TokenHelper tokenHelper) {
            _authService = authService;
            _context = context;
            _tokenHelper = tokenHelper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            var result = await _authService.RegisterUserAsync(registerUserDto);

            if (!result.IsValid) return BadRequest(result.ErrorResponse);

            return Ok(new { message = "User registered successfully!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            ValidationResult validation = AuthValidator.ValidateLogin(loginUserDto);

            if (!validation.IsValid) return BadRequest(validation.ErrorResponse);
          
            User? userFound = await _context.Users.FirstOrDefaultAsync(
                (user) => user.Email == loginUserDto.Email);

            if (userFound != null && BCrypt.Net.BCrypt.Verify(loginUserDto.Password, userFound.Password)) 
            {
                var token = _tokenHelper.GenerateToken(userFound.Id);
                return Ok(new { message = "Success!", token }); 
            }

            return BadRequest(new { message = "Wrong credentials, please try again!" });
        }
    }
}
