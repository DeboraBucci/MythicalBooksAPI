using MythicalBooksAPI.Dtos;
using MythicalBooksAPI.Models.Entities;

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
