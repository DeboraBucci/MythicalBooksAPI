using Microsoft.EntityFrameworkCore;
using MythicalBooksAPI.Data.Contexts;
using MythicalBooksAPI.Dtos;
using MythicalBooksAPI.Dtos.Books;
using MythicalBooksAPI.Interfaces.Repositories;
using MythicalBooksAPI.Mappers;
using MythicalBooksAPI.Models.Books;

namespace MythicalBooksAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            _context = context;
        }

        public IQueryable<Book> GetQueryableBooks() => 
            _context
                .Books
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .AsNoTracking()
                .AsQueryable();

        public async Task<int> CountQueryableAsync<T>(IQueryable<T> countable) => 
            await countable.CountAsync();

        public async Task<Book?> GetByIdAsync(int id) => await _context.Books
            .AsNoTracking()
            .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .Include(b => b.BookPublishers)
                .ThenInclude(bp => bp.Publisher)
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}
