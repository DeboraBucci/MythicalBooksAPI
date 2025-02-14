namespace MythicalBooksAPI.Dtos
{
    public class BookDetailDto
    {
        public int Id { get; set; }
        public int? RatingCount { get; set; }
        public int? Pages { get; set; }
        public int? Stock { get; set; }
        public int? PublishedYear { get; set; }

        public double? Weight { get; set; }
        public double? AverageRating { get; set; }
        public double? Price { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? ISBN13 { get; set; }
        public string? ISBN10 { get; set; }
        public string? Image { get; set; }
        public required string Language { get; set; }
        public string? PhysicalFormat { get; set; }

        public List<AuthorDto> Authors { get; set; } = new List<AuthorDto>();
        public List<PublisherDto> Publishers { get; set; } = new List<PublisherDto>();
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    }
}
