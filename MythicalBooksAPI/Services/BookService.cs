using Microsoft.EntityFrameworkCore;
using MythicalBooksAPI.Dtos;
using MythicalBooksAPI.Dtos.Books;
using MythicalBooksAPI.Interfaces.Repositories;
using MythicalBooksAPI.Interfaces.Services;
using MythicalBooksAPI.Mappers;
using MythicalBooksAPI.Models.Books;

namespace MythicalBooksAPI.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository) { 
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }


        public async Task<PagedResultDto<BookDto>?> 
            GetBooksAsync(
                string? search, 
                List<int> categories, 
                int page = 1, 
                int pageSize = 2
            )
        {
            try
            {
                var query = _bookRepository.GetQueryableBooks();

                if (categories != null && categories.Count == 0)
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

                int totalItems = await _bookRepository.CountQueryableAsync(query);
                int totalPages = (int) Math.Ceiling(totalItems / (double)pageSize);

                var books = await query
                    .OrderBy(b => b.Id) // Important: apply OrderBy before Skip/Take
                    .Skip((page - 1) * (pageSize))
                    .Take(pageSize)
                    .Select(b => BookMapper.ToBookDto(b))
                    .ToListAsync();

                if (books.Count == 0)
                    throw new Exception();

                var response = new PagedResultDto<BookDto>
                {
                    Page = page,
                    PageSize = pageSize,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    Data = books
                };

                return response;
            }

            catch
            {
                return null;
            }
        }


        public async Task<BookDetailDto?> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
                return null;

            return book.ToBookDetailDto();
        }

        public async Task<List<CategoryDto>?> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();

                if (categories.Count == 0)
                    throw new Exception();

                return categories.Select(c => c.ToCategoryDto()).ToList();
            }

            catch
            {
                return null;
            }
        }
       
    
    }
}
