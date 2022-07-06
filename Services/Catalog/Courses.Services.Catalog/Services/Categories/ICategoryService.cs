using Courses.Services.Catalog.Dto;
using Courses.Shared.Dtos;

namespace Courses.Services.Catalog.Services.Categories
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();

        Task<Response<CategoryDto>> CreateAsync(CategoryDto category);

        Task<Response<CategoryDto>> GetByIdAsync(string Id);
    }
}