using FreeCourseServices.Order.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourseServices.Order.Domain.OrderAggregate
{
    public class OrderItem : Entity
    {
        public OrderItem()
        {

        }

        public OrderItem(string productId, string productName, string pictureUrl, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
            Price = price;
        }

        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string PictureUrl { get; private set; }
        public decimal Price { get; private set; }

        public void UpdateOrderItem(string productName, string pictureUrl, decimal price)
        {
            this.ProductName = productName;
            this.PictureUrl = pictureUrl;
            this.Price = price;
        }
    }
}
