using FreeCourse.Shared.ControllerBases;
using FreeCourseServices.Catalog.Dtos;
using FreeCourseServices.Catalog.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourseServices.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            
            return CreateActionResultInstance(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();
            
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto category)
        {
            var response = await _categoryService.CreateAsync(category);

            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto category)
        {
            var response = await _categoryService.UpdateAsync(category);

            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _categoryService.DeleteAsync(id);

            return CreateActionResultInstance(response);
        }
    }
}
