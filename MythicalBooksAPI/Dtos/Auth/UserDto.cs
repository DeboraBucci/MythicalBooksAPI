using MythicalBooksAPI.Models.Auth;

namespace MythicalBooksAPI.Dtos.Auth
{
    public class UserDto
    {
        public string? Username { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public int? PostalCode { get; set; }

        public UserSubscription? ActiveSubscription { get; set; }

    }
}
