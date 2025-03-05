using MythicalBooksAPI.Dtos;
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
    }
}
