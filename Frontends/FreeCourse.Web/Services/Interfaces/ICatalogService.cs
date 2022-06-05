using FreeCourse.Web.Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<CourseViewModel> GetByCourseId(string courseId);
        Task<List<CategoryViewModel>> GetAllCategoriesAsync();
        Task<List<CourseViewModel>> GetAllCourseAsync();
        Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId);

        Task<bool> CreateCourseAsync(CourseCreateInput input);
        Task<bool> UpdateCourseAsync(CourseUpdateInput input);
        Task<bool> DeleteCourseAsync(string courseId);
        
    }
}
