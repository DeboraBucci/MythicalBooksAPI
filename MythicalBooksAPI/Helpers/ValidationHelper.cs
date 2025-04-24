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
    }
}
