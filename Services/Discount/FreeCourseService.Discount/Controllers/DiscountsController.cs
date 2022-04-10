using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Services;
using FreeCourseService.Discount.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourseService.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomBaseController
    {
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public DiscountsController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
        {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var discounts = await _discountService.GetAll();

            return CreateActionResultInstance(discounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var discount = await _discountService.GetById(id);

            return CreateActionResultInstance(discount);
        }

        [HttpGet]
        [Route("/api/[controller]/getbycode/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var userId = _sharedIdentityService.GetUserId;
            var discount = await _discountService.GetByCodeAndUserId(code.Trim(), userId);

            return CreateActionResultInstance(discount);
        }

        [HttpGet]
        [Route("/api/[controller]/getbycode/{code}/{userid}")]
        public async Task<IActionResult> GetByCode(string code, string userid)
        {
            var discount = await _discountService.GetByCodeAndUserId(code.Trim(), userid);

            return CreateActionResultInstance(discount);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Models.Discount discount)
        {
            var result = await _discountService.Save(discount);

            return CreateActionResultInstance(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Models.Discount discount)
        {
            var result = await _discountService.Update(discount);

            return CreateActionResultInstance(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _discountService.Delete(id);

            return CreateActionResultInstance(result);
        }

    }
}
