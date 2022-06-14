using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourseService.FakePayment.Models
{
    public class PaymentDto
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CCV { get; set; }
        public decimal TotalPrice { get; set; }

        public OrderDto Order { get; set; }
    }
}
