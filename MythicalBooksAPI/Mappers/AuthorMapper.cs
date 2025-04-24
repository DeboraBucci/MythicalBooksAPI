using MythicalBooksAPI.Dtos.Books;
using MythicalBooksAPI.Models.Books;

namespace MythicalBooksAPI.Mappers
{
    public static class AuthorMapper
    {
        /// <summary>
        /// This method takes an Author instance, converts it to an AuthorDto, and then returns it.
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
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
