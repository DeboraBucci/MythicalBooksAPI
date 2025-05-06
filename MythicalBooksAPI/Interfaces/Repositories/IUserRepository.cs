using MythicalBooksAPI.Models.Auth;

namespace MythicalBooksAPI.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync(string email);
        void AddUser(User user);
        Task SaveChangesAsync();
    }
}
