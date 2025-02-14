using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MythicalBooksAPI.Data;
using MythicalBooksAPI.Dtos;
using MythicalBooksAPI.Mappers;
using MythicalBooksAPI.Models.Entities;

namespace MythicalBooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _context
                .Books
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .Include(b => b.BookPublishers)
                    .ThenInclude(bp => bp.Publisher)
                .Select(b => BookMapper.ToBookDto(b))
                .ToListAsync();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var book = await _context.Books
            .AsNoTracking() // Optimizes performance
            .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
            .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
            .Include(b => b.BookPublishers).ThenInclude(bp => bp.Publisher)
            .Where(b => b.Id == id)
            .FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book.ToBookDto());
        }
    }
}
