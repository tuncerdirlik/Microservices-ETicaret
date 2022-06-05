using FreeCourse.Web.Models.Baskets;
using FreeCourse.Web.Models.Discounts;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public BasketController(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            var basketViewModel = await _basketService.Get();

            return View(basketViewModel);
        }

        public async Task<IActionResult> AddBasketItem(string courseId)
        {
            var courseItem = await _catalogService.GetByCourseId(courseId);
            var basketItem = new BasketItemViewModel
            {
                CourseId = courseItem.Id,
                CourseName = courseItem.Name,
                Price = courseItem.Price
            };

            await _basketService.AddBasketItem(basketItem);

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> RemoveBasketItem(string courseId)
        {
            await _basketService.RemoveBasketItem(courseId);

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> ApplyDiscount(string discountCode)
        {
            var discountStatus = await _basketService.ApplyDiscount(discountCode);
            TempData["discountStatus"] = discountStatus;

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CancelAppliedDiscount()
        {
            await _basketService.CancelAppliedDiscount();

            return RedirectToAction("Index");
        }
    }
}
