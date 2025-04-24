using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MythicalBooksAPI.Models.Auth;
using MythicalBooksAPI.Mappers;
using MythicalBooksAPI.Helpers;
using System.Text.RegularExpressions;
using MythicalBooksAPI.Data.Contexts;
using MythicalBooksAPI.Dtos.Auth;
using MythicalBooksAPI.Helpers.Validators;

namespace MythicalBooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthContext _context;
        private readonly TokenHelper _tokenHelper;

        public AuthController(AuthContext context, TokenHelper tokenHelper) {
            _context = context;
            _tokenHelper = tokenHelper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            ValidationResult validation = AuthValidator.ValidateRegister(registerUserDto);
            if (!validation.IsValid) return BadRequest(validation.ErrorResponse);

            if (await _context.Users.AnyAsync(u => u.Email == registerUserDto.Email))
            {
                return BadRequest(new { message = "Unable to register user, please try again later." });
            }

            // TODO: BCrypt -> ARGON2ID
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password);

            User user = registerUserDto.ToUser(hashedPassword);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            if (!ValidationHelper.IsValidEmail(loginUserDto.Email))
            {
                return BadRequest("Invalid email format");
            }

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
