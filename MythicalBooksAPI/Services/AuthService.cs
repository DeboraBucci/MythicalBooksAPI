using Microsoft.EntityFrameworkCore;
using MythicalBooksAPI.Dtos.Auth;
using MythicalBooksAPI.Helpers.Validators;
using MythicalBooksAPI.Interfaces.Repositories;
using MythicalBooksAPI.Interfaces.Services;
using MythicalBooksAPI.Mappers;
using MythicalBooksAPI.Models.Auth;
using MythicalBooksAPI.Repositories;

namespace MythicalBooksAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
    }
}

