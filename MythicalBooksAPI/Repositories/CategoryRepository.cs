using Microsoft.EntityFrameworkCore;
using MythicalBooksAPI.Data.Contexts;
using MythicalBooksAPI.Dtos.Books;
using MythicalBooksAPI.Interfaces.Repositories;
using MythicalBooksAPI.Models.Books;

namespace MythicalBooksAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LibraryContext _context;

        public CategoryRepository(LibraryContext context)
        {
            _context = context;
        }

        public Task<List<Category>> GetAllAsync() => _context.Categories.ToListAsync();
    }
}
