using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MythicalBooksAPI.Data;
using MythicalBooksAPI.Mappers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public async Task<IActionResult> GetBooks([FromQuery] string? search)
        {
            var query = _context
                            .Books
                                .Include(b => b.BookAuthors)
                                    .ThenInclude(ba => ba.Author)
                            .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(b =>
                    b.Title.Contains(search) ||
                    (b.ISBN10 ?? "").Contains(search) ||
                    (b.ISBN13 ?? "").Contains(search) ||
                    b.BookAuthors.Any(ba => ba.Author.Name.Contains(search)) ||
                    b.BookPublishers.Any(bp => bp.Publisher.Name.Contains(search))
                );
            }

            var books = await query
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

            return Ok(book.ToBookDetailDto());
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                return Ok(await _context.Categories.ToListAsync());
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("categories/{id}")]
        public async Task<IActionResult> GetCategoryBooks([FromRoute] int id)
        {
            try
            {
                var categoryBooks =
                     await _context
                    .Books
                    .Include(b => b.BookAuthors)
                        .ThenInclude(ba => ba.Author)
                    .Where(b => b.BookCategories.Any(bc => bc.CategoryId == id))
                    .Select(b => BookMapper.ToBookDto(b))
                    .ToListAsync();

                return Ok(categoryBooks);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
