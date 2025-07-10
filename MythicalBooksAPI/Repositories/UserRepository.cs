using Microsoft.EntityFrameworkCore;
using MythicalBooksAPI.Data.Contexts;
using MythicalBooksAPI.Interfaces.Repositories;
using MythicalBooksAPI.Models.User;

namespace MythicalBooksAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthContext _context;

        public UserRepository(AuthContext context)
        {
            _context = context;
        }

        public Task<bool> EmailExistsAsync(string email)
            => _context.Users.AnyAsync(u => u.Email == email);

        public void AddUser(User user)
            => _context.Users.Add(user);

        public Task<User?> GetUserAsync(string email)
            => _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public Task SaveChangesAsync()
            => _context.SaveChangesAsync();
    }
}
