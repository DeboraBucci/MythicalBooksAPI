using MythicalBooksAPI.Models.Books;

namespace MythicalBooksAPI.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
    }
}
