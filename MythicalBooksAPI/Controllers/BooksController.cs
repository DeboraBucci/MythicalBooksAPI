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
        public async Task<IActionResult> GetBooks(
            [FromQuery] string? search,
            [FromQuery] List<int> categories,
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 2)
        {
            var query = _context
                        .Books
                        .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                        .AsQueryable();

            if (categories != null && categories.Any())
            {
                query = query.Where(
                    b => categories.All(
                        cid => b.BookCategories.Any(
                            bc => bc.CategoryId == cid
                            )
                        )
                    );
            }

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

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var books = await query
                .OrderBy(b => b.Id) // Important: apply OrderBy before Skip/Take
                .Skip((page - 1) * (pageSize))
                .Take(pageSize)
                .Select(b => BookMapper.ToBookDto(b))
                .ToListAsync();

            var response = new
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Data = books
            };

            return Ok(response);

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

        [HttpGet("books-ids")]
        public async Task<IActionResult> GetBooksByIds([FromQuery] List<int> ids)
        {
            var books = await _context.Books
            .AsNoTracking()
            .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
            .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
            .Include(b => b.BookPublishers).ThenInclude(bp => bp.Publisher)
            .Where(b => ids.Contains(b.Id))
            .ToListAsync();


            if (!books.Any())
            {
                return NotFound();
            }

            var bookDtos = books.Select(b => b.ToBookDto());

            return Ok(bookDtos);
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

        [HttpGet("categories-books")]
        public async Task<IActionResult> GetCategoryBooks([FromQuery] List<int> ids)
        {
            try
            {
                var categoryBooks =
                     await _context
                    .Books
                    .Include(b => b.BookAuthors)
                        .ThenInclude(ba => ba.Author)
                    .Where(b => ids.All(id => b.BookCategories.Any(bc => bc.CategoryId == id)))
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
