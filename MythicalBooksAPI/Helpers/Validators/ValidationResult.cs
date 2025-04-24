namespace MythicalBooksAPI.Helpers.Validators
{
    public class ValidationResult
    {
        public bool IsValid { get; set; } = true;
        public object? ErrorResponse { get; set; } = null;

        public static ValidationResult Success() => new();
        public static ValidationResult Fail(object error) => new () { IsValid = false, ErrorResponse = error };
    }
}
