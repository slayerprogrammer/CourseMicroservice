using Courses.Services.Catalog.Dto;
using Courses.Shared.Dtos;

namespace Courses.Services.Catalog.Services.Courses
{
    public interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();

        Task<Response<CourseDto>> GetByIdAsync(string id);

        Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId);

        Task<Response<CourseDto>> CreateAsync(CourseCreateDto createCourseDto);

        Task<Response<NoContent>> UpdateAsync(CourseUpdateDto updateCourseDto);

        Task<Response<NoContent>> DeleteAsync(string id);
    }
}