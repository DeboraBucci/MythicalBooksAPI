namespace MythicalBooksAPI.Helpers.Validators
{
    public class ValidationResult
    {
        public bool IsValid { get; set; } = true;

        public ValidationError? ErrorResponse { get; set; } = null;

        public static ValidationResult Success() => new();
        public static ValidationResult Fail(ValidationError error) => new () { IsValid = false, ErrorResponse = error };
    }
}
