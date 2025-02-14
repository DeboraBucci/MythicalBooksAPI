using MythicalBooksAPI.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MythicalBooksAPI.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public int? RatingCount { get; set; }
        public int? Stock { get; set; }
        public double? AverageRating { get; set; }
        public double? Price { get; set; }
        public required string Title { get; set; }
        public string? Image { get; set; }
        public required string Language { get; set; }

        public List<AuthorDto> Authors { get; set; } = new List<AuthorDto>();
    }
}
