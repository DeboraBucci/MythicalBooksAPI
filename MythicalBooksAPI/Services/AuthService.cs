using Microsoft.EntityFrameworkCore;
using MythicalBooksAPI.Dtos.Auth;
using MythicalBooksAPI.Helpers;
using MythicalBooksAPI.Helpers.Validators;
using MythicalBooksAPI.Interfaces.Repositories;
using MythicalBooksAPI.Interfaces.Services;
using MythicalBooksAPI.Mappers;
using MythicalBooksAPI.Models.Auth;
using MythicalBooksAPI.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace MythicalBooksAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenHelper _tokenHelper;

        public AuthService(IUserRepository userRepository, TokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }

        public async Task<ValidationResult> RegisterUserAsync(RegisterUserDto userDto)
        {
            ValidationResult validation = AuthValidator.ValidateRegister(userDto);

            if (!validation.IsValid) return validation;

            if (await _userRepository.EmailExistsAsync(userDto.Email))
                return ValidationResult.Fail(
                    new ValidationError("Email is already in use"));

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            User user = userDto.ToUser(hashedPassword);  

            _userRepository.AddUser(user);
            await _userRepository.SaveChangesAsync();

            return ValidationResult.Success();
        }

        public async Task<AuthResponse?> LoginUserAsync(LoginUserDto userDto)
        {
            ValidationResult validation = AuthValidator.ValidateLogin(userDto);

            if (!validation.IsValid) return null;

            User? user = await _userRepository.GetUserAsync(userDto.Email);

            if(user != null && BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password))
            {
                var token = _tokenHelper.GenerateToken(user.Id);
                return new AuthResponse(token, "User logged successfully!");
            }

            return null;
        }
    }
}

