using Microsoft.EntityFrameworkCore;
using MythicalBooksAPI.Models.Books;

namespace MythicalBooksAPI.Data
{
    public class LibraryContext : DbContext
    {

        public LibraryContext(DbContextOptions<LibraryContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookPublisher> BookPublishers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-Many: Book - Author
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);

            // Many-to-Many: Book - Category
            modelBuilder.Entity<BookCategory>()
                .HasKey(bc => new { bc.BookId, bc.CategoryId });

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCategories)
                .HasForeignKey(bc => bc.BookId);

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(bc => bc.CategoryId);

            // Many-to-Many: Book - Publisher
            modelBuilder.Entity<BookPublisher>()
                .HasKey(bp => new { bp.BookId, bp.PublisherId });

            modelBuilder.Entity<BookPublisher>()
                .HasOne(bp => bp.Book)
                .WithMany(b => b.BookPublishers)
                .HasForeignKey(bp => bp.BookId);

            modelBuilder.Entity<BookPublisher>()
                .HasOne(bp => bp.Publisher)
                .WithMany(p => p.BookPublishers)
                .HasForeignKey(bp => bp.PublisherId);
        }
    }
}
