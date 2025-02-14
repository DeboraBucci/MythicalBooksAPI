using MythicalBooksAPI.Dtos;
using MythicalBooksAPI.Models.Entities;

namespace MythicalBooksAPI.Mappers
{
    public static class BookMapper
    {
        public static BookDetailDto ToBookDetailDto (this Book book)
        {
            return new BookDetailDto
            {
                Id = book.Id,
                RatingCount = book.RatingCount,
                Pages = book.Pages,
                Stock = book.Stock,
                Weight = book.Weight,
                AverageRating = book.AverageRating,
                Price = book.Price,
                Title = book.Title,
                Description = book.Description,
                ISBN10 = book.ISBN10,
                ISBN13 = book.ISBN13,
                Image = book.Image,
                Language = book.Language,
                PhysicalFormat = book.PhysicalFormat,
                PublishedYear = book.PublishedYear,
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

        public static BookDto ToBookDto(this Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                RatingCount = book.RatingCount,
                Stock = book.Stock,
                AverageRating = book.AverageRating,
                Price = book.Price,
                Title = book.Title,
                Image = book.Image,
                Language = book.Language,
                Authors =
                    book.BookAuthors
                    .Select(ba => AuthorMapper.ToAuthorDto(ba.Author))
                    .ToList(),
            };
        }
    }
}
