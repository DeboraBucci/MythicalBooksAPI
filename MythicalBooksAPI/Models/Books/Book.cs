using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MythicalBooksAPI.Models.Books
{
    public class Book
    {
        public int Id { get; set; }
        public int? RatingCount { get; set; }
        public int? Pages { get; set; }
        public int? Stock { get; set; }
        public int? PublishedYear { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public double? Weight { get; set; }
        [Range(0, 5)]
        public double? AverageRating { get; set; }
        public double? Price { get; set; }
        [MaxLength(100)]
        public required string Title { get; set; }
        [MaxLength(1000)]
        public string? Description { get; set; }
        public string? ISBN13 { get; set; }
        public string? ISBN10 { get; set; }
        public string? Image { get; set; }
        public required string Language { get; set; }
        public string? PhysicalFormat { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
        public ICollection<BookPublisher> BookPublishers { get; set; } = new List<BookPublisher>();
    }
}
