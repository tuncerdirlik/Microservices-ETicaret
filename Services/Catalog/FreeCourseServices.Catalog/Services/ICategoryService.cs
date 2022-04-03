using FreeCourse.Shared.Dtos;
using FreeCourseServices.Catalog.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourseServices.Catalog.Services
{
    public interface ICategoryService
    {
        Task<Response<CategoryDto>> GetByIdAsync(string id);

        Task<Response<List<CategoryDto>>> GetAllAsync();

        Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        Task<Response<NoContent>> UpdateAsync(CategoryDto categoryDto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
