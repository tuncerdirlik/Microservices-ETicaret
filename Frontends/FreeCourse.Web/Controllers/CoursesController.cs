using FreeCourse.Shared.Services;
using FreeCourse.Web.Models.Catalogs;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Web.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CoursesController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            var lstCourses = await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId);

            return View(lstCourses);
        }

        public async Task<IActionResult> Create()
        {
            var lstCategories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(lstCategories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInput courseCreateInput)
        {
            var lstCategories = await _catalogService.GetAllCategoriesAsync();

            if (!ModelState.IsValid)
            {
                ViewBag.categoryList = new SelectList(lstCategories, "Id", "Name");
                return View(courseCreateInput);
            }

            courseCreateInput.UserId = _sharedIdentityService.GetUserId;

            await _catalogService.CreateCourseAsync(courseCreateInput);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var course = await _catalogService.GetByCourseId(id);

            if (course == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var lstCategories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(lstCategories, "Id", "Name", course.Id);


            CourseUpdateInput courseUpdateInput = new CourseUpdateInput
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
                Feature = new FeatureViewModel { Duration = course.Feature.Duration },
                CategoryId = course.CategoryId,
                UserId = course.UserId,
                Picture = course.Picture
            };

            return View(courseUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateInput courseUpdateInput)
        {
            var lstCategories = await _catalogService.GetAllCategoriesAsync();

            if (!ModelState.IsValid)
            {
                ViewBag.categoryList = new SelectList(lstCategories, "Id", "Name");
                return View(courseUpdateInput);
            }

            await _catalogService.UpdateCourseAsync(courseUpdateInput);

            return RedirectToAction(nameof(Index));

        }

        
        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteCourseAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
