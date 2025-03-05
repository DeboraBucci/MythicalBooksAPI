using MythicalBooksAPI.Dtos;
using MythicalBooksAPI.Models.Books;

namespace MythicalBooksAPI.Mappers
{
    public static class AuthorMapper
    {
        public static AuthorDto ToAuthorDto (this Author author)
        {
            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Fuller_Name = author.Fuller_Name,
                Bio = author.Bio,
                Birth_Date = author.Birth_Date,
                Death_Date = author.Death_Date,
            };
        }
    }
}
