using Courses.Services.Catalog.Dto;
using Courses.Services.Catalog.Models;
using Courses.Shared.Dtos;

namespace Courses.Services.Catalog.Services.Categories
{
    interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(Category category);
        Task<Response<CategoryDto>> GetByIdAsync(string Id);

    }
}
