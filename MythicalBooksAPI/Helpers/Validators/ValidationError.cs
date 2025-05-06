namespace MythicalBooksAPI.Helpers.Validators
{
    public class ValidationError
    {
        public string Message { get; set; } = "";
        public List<string>? Fields { get; set; } = null;

        public ValidationError(string message)
        {
            Message = message;
        }

        public ValidationError(string message, List<string> fields) {
            Message = message;
            Fields = fields;
        }
    }
}
