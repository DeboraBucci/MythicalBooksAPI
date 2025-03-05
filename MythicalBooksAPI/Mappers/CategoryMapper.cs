using MythicalBooksAPI.Dtos;
using MythicalBooksAPI.Models.Books;

namespace MythicalBooksAPI.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ToCategoryDto (this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            };
        }
    }
}
