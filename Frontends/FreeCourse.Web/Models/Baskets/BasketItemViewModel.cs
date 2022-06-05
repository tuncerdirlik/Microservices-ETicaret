using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Web.Models.Baskets
{
    public class BasketItemViewModel
    {
        public int Quantity { get; set; } = 1;
        
        public string CourseId { get; set; }
        
        public string CourseName { get; set; }
        
        public decimal Price { get; set; }
        
        private decimal? DiscountedPrice { get; set; }

        public decimal GetCurrentPrice => this.DiscountedPrice.HasValue ? this.DiscountedPrice.Value : this.Price;

        public void AppliedDiscount(decimal discountPrice)
        {
            this.DiscountedPrice = discountPrice;
        }

    }
}
