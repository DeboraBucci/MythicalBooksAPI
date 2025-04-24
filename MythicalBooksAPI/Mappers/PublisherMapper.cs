using MythicalBooksAPI.Dtos.Books;
using MythicalBooksAPI.Models.Books;

namespace MythicalBooksAPI.Mappers
{
    public static class PublisherMapper
    {
        public static PublisherDto ToPublisherDto(this Publisher publisher)
        {
            return new PublisherDto
            {
                Id = publisher.Id,
                Name = publisher.Name,
            };
        }
    }
}
