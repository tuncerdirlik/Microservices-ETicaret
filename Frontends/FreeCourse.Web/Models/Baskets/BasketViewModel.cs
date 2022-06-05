using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Web.Models.Baskets
{
    public class BasketViewModel
    {
        public string UserId { get; set; }
        
        public string DiscountCode { get; set; }
        
        public int? DiscountRate { get; set; }

        private List<BasketItemViewModel> _basketItems { get; set; }
        
        public List<BasketItemViewModel> BasketItems
        {
            get
            {
                if (this.HasDiscount)
                {
                    this._basketItems.ForEach(k =>
                    {
                        var discountPrice = k.Price * ((decimal)this.DiscountRate.Value / 100);
                        k.AppliedDiscount(Math.Round(k.Price - discountPrice, 2));
                    });
                }

                return this._basketItems;
            }
            set
            {
                _basketItems = value;
            }
        }

        public decimal TotalPrice
        {
            get => BasketItems.Sum(k => k.Quantity * k.GetCurrentPrice);
        }

        public bool HasDiscount
        {
            get
            {
                return !string.IsNullOrEmpty(this.DiscountCode) && this.DiscountRate.HasValue;
            }
        }

        public void CancelDiscount()
        {
            this.DiscountCode = string.Empty;
            this.DiscountRate = null;
        }
    }
}
