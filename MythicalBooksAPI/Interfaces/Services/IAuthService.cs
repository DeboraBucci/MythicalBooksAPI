using MythicalBooksAPI.Dtos.Auth;
using MythicalBooksAPI.Helpers.Validators;

namespace MythicalBooksAPI.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ValidationResult> RegisterUserAsync(RegisterUserDto userDto);
    }
}
