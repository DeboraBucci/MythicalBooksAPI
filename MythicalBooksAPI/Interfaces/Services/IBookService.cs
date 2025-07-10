using MythicalBooksAPI.Dtos.Books;
using MythicalBooksAPI.Dtos;

namespace MythicalBooksAPI.Interfaces.Services
{
    public interface IBookService
    {
        Task<PagedResultDto<BookDto>?>
            GetBooksAsync(
                string? search,
                List<int> categories,
                int page = 1,
                int pageSize = 2
            );

        Task<BookDetailDto?> GetBookByIdAsync(int id);

        Task<List<CategoryDto>?> GetAllCategoriesAsync();
    }
}
