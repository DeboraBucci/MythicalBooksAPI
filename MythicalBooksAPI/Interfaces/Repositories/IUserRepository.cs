using MythicalBooksAPI.Models.Auth;

namespace MythicalBooksAPI.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync(string email);
        Task<User?> GetUserAsync(string email);
        void AddUser(User user);
        Task SaveChangesAsync();
    }
}
