using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MythicalBooksAPI.Data.Contexts;
using MythicalBooksAPI.Dtos;
using MythicalBooksAPI.Dtos.Books;
using MythicalBooksAPI.Interfaces.Services;
using MythicalBooksAPI.Mappers;
using MythicalBooksAPI.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MythicalBooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context, IBookService bookService)
        {
            _context = context;
            _bookService = bookService;
        }


        [HttpGet]
        public async Task<IActionResult> GetBooks(
            [FromQuery] string? search,
            [FromQuery] List<int> categories,
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 2)
        {
            var response = await _bookService.GetBooksAsync(search, categories, page, pageSize);

            if (response == null)
                return NotFound("No books found.");

            return Ok(response);

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound("There is no book with such id");
            }

            return Ok(book);
        }


        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _bookService.GetAllCategoriesAsync();

            if (categories == null)
                return BadRequest("No categories found");

            return Ok(categories);
        }
    }
}
