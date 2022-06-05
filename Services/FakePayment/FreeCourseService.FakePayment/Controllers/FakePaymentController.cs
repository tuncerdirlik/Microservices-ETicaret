using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using FreeCourseService.FakePayment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourseService.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentController : CustomBaseController
    {
        [HttpPost]
        public IActionResult MakePayment(PaymentDto paymentDto)
        {
            return CreateActionResultInstance(Response<NoContent>.Success(FreeCourse.Shared.Enums.ResponseStatusCodes.Ok));
        }
    }
}
