using MythicalBooksAPI.Dtos;
using MythicalBooksAPI.Models.Entities;

namespace MythicalBooksAPI.Mappers
{
    public static class BookMapper
    {
        public static BookDto ToBookDto (this Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Ratings = book.Ratings,
                Pages = book.Pages,
                Stock = book.Stock,
                Weight = book.Weight,
                Rating = book.Rating,
                Price = book.Price,
                Title = book.Title,
                Description = book.Description,
                ISBN = book.ISBN,
                Image = book.Image,
                Language = book.Language,
                PhysicalFormat = book.PhysicalFormat,

                Authors = 
                    book.BookAuthors
                    .Select(ba => AuthorMapper.ToAuthorDto(ba.Author))
                    .ToList(),

                Categories = 
                    book.BookCategories
                    .Select(bc => CategoryMapper.ToCategoryDto(bc.Category))
                    .ToList(),

                Publishers = 
                    book.BookPublishers
                    .Select(bp => PublisherMapper.ToPublisherDto(bp.Publisher))
                    .ToList(),
            };
        }
    }
}
