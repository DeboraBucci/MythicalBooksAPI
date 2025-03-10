using System.ComponentModel.DataAnnotations;

namespace MythicalBooksAPI.Models.Auth
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string Password {  get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public int? PostalCode { get; set; }

        public ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
        public bool HasActiveSubscription => UserSubscriptions.Any(s => s.EndDate > DateTime.UtcNow);
    }
}
