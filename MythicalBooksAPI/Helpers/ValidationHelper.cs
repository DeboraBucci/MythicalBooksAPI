using MythicalBooksAPI.Dtos.Auth;
using System.Text.RegularExpressions;

namespace MythicalBooksAPI.Helpers
{
    public static class ValidationHelper
    {
        private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled); // ?

        public static bool IsValidEmail(string email)
        {
            return
                 (!string.IsNullOrWhiteSpace(email) && EmailRegex.IsMatch(email));
        }

        public static bool IsNotEmpty(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        public static bool HasMinLength(string input, int minLength)
        {
            return input?.Length >= minLength;
        }

        public static bool HasMaxLength(string input, int maxLength)
        {
            return input?.Length <= maxLength;
        }
    }
}
