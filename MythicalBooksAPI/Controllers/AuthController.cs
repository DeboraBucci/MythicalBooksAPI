using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MythicalBooksAPI.Data;
using MythicalBooksAPI.Models.Auth;
using MythicalBooksAPI.Dtos;
using MythicalBooksAPI.Mappers;
using MythicalBooksAPI.Helpers;
using System.Text.RegularExpressions;

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
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto request)
        {
            if (request.Name.Trim().Length == 0 
                || request.Surname.Trim().Length == 0 
                || request.Email.Trim().Length == 0 
                || request.Password.Trim().Length == 0 
                || request.Country.Trim().Length == 0)
            {
                return BadRequest("Incomplete credentials, please try again");
            }
            
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(request.Email, emailPattern))
            {
                return BadRequest("Invalid email format");
            }

            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return BadRequest(new { message = "Unable to register user, please try again later." });
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            User user = request.ToUser(hashedPassword);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] AuthRequest request)
        {
            User? userFound = await _context.Users.FirstOrDefaultAsync(
                (user) => user.Email == request.Email);

            if (userFound != null && BCrypt.Net.BCrypt.Verify(request.Password, userFound.Password)) 
            {
                var token = _tokenHelper.GenerateToken(userFound.Id);
                return Ok(new { message = "Success!", token }); 
            }

            return BadRequest(new { message = "Wrong credentials, please try again!" });
        }

    }
}
