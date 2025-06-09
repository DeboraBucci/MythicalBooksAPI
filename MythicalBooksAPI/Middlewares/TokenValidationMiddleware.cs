using MythicalBooksAPI.Helpers;

namespace MythicalBooksAPI.Middlewares
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, TokenHelper tokenHelper)
        {
            var token = context
                .Request
                .Headers["Authorization"]
                .FirstOrDefault()?
                .Split(" ")
                .Last();

            if (string.IsNullOrEmpty(token) || !tokenHelper.IsTokenValid(token))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token is invalid or expired.");
                return;
            }

            await _next(context);
        }
    }
}
