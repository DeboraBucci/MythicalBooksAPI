using MythicalBooksAPI.Dtos.Auth;
using MythicalBooksAPI.Models.Auth;

namespace MythicalBooksAPI.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this RegisterUserDto request, string hashedPassword)
        {
            return new User
            {
                Email = request.Email,
                Password = hashedPassword,
                Username = request.Username,
                Name = request.Name,
                Surname = request.Surname,
                DateOfBirth = request.DateOfBirth,
                Address = request.Address,
                Country = request.Country,
                City = request.City,
                PostalCode = request.PostalCode
            };
        }

        public static UserDto UserToUserDto(this User user)
        {
            return new UserDto
            {
                Username = user.Username,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Name = user.Name,
                Surname = user.Surname,
                Address = user.Address,
                Country = user.Country,
                City = user.City,
                PostalCode = user.PostalCode,
                ActiveSubscription = user.HasActiveSubscription ? user.UserSubscriptions.LastOrDefault() : null,
            };
        }
    }
}

