using Microsoft.AspNetCore.Mvc;
using MythicalBooksAPI.Dtos;
using MythicalBooksAPI.Dtos.Books;
using MythicalBooksAPI.Models.Books;

namespace MythicalBooksAPI.Interfaces.Repositories
{
    public interface IBookRepository
    {
        IQueryable<Book> GetQueryableBooks();

        Task<int> CountQueryableAsync<T>(IQueryable<T> countable);

        Task<Book?> GetByIdAsync(int id);
    }

}