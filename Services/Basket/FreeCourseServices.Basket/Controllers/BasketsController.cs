﻿using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Services;
using FreeCourseServices.Basket.Dtos;
using FreeCourseServices.Basket.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourseServices.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var basketItem = await _basketService.GetBasket(_sharedIdentityService.GetUserId);

            return CreateActionResultInstance(basketItem);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdate(BasketDto basketDto)
        {
            basketDto.UserId = _sharedIdentityService.GetUserId;

            var response = await _basketService.SaveOrUpdate(basketDto);

            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
        {
            var response = await _basketService.Delete(_sharedIdentityService.GetUserId);

            return CreateActionResultInstance(response);
        }
    }
}
