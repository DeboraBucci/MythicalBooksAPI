namespace MythicalBooksAPI.Models.Entities
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
        public ICollection<BookPublisher> BookPublishers { get; set; } = new List<BookPublisher>();
    }
}
