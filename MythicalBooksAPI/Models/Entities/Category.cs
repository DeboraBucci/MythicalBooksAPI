namespace MythicalBooksAPI.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
        public ICollection<BookPublisher> BookPublishers { get; set; } = new List<BookPublisher>();
    }
}
