namespace MythicalBooksAPI.Models.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Fuller_Name { get; set; } = string.Empty;
        public string? Bio { get; set; } = string.Empty;
        public DateTime? Birth_Date { get; set; }
        public DateTime? Death_Date { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
