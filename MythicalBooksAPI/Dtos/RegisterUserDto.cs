using MythicalBooksAPI.Models.Auth;

namespace MythicalBooksAPI.Dtos
{
    public class RegisterUserDto
    {
        public string? Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }

        public int? PostalCode {  get; set; }
    }
}
