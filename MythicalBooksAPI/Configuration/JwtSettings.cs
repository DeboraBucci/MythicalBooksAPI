namespace MythicalBooksAPI.Helpers
{
    public class JwtSettings
    {
        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public int ExpiryMinutes { get; set; }
    }
}
