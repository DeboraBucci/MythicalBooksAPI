namespace MythicalBooksAPI.Dtos
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public  string Name { get; set; } = string.Empty;
        public string? Fuller_Name { get; set; } = string.Empty;
        public string? Bio { get; set; } = string.Empty;
        public DateTime? Birth_Date { get; set; }
        public DateTime? Death_Date { get; set; }
    }
}
