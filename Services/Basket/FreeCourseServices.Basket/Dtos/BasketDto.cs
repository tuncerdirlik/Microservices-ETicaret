using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourseServices.Basket.Dtos
{
    public class BasketDto
    {
        public string UserId { get; set; }
        
        public string DiscountCode { get; set; }
        
        public int? DiscountRate { get; set; }
        
        public List<BasketItemDto> BasketItems { get; set; }
        
        public decimal TotalPrice
        {
            get => BasketItems.Sum(k => k.Quantity * k.Price);
        }
    }
}
