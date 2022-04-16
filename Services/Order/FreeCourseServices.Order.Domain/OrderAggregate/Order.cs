using FreeCourseServices.Order.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourseServices.Order.Domain.OrderAggregate
{
    public class Order : Entity, IAggreagateRoot
    {
        public Order()
        {

        }

        public Order(string buyerId, Address address)
        {
            _orderItems = new List<OrderItem>();

            this.CreatedDate = DateTime.Now;
            this.BuyerId = buyerId;
            this.Address = address;
        }

        public DateTime CreatedDate { get; private set; }
        public Address Address { get; private set; }
        public string BuyerId { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
        {
            var existsProduct = _orderItems.Any(k => k.ProductId == productId);
            if (!existsProduct)
            {
                _orderItems.Add(new OrderItem(productId, productName, pictureUrl, price));
            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(k => k.Price);
    }
}
