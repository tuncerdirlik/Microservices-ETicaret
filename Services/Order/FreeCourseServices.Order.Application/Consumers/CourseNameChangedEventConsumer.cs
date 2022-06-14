using FreeCourse.Shared.Messages;
using FreeCourseServices.Order.Infrastructure;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourseServices.Order.Application.Consumers
{
    public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        private readonly OrderDbContext _orderDbContext;

        public CourseNameChangedEventConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
            var orderItems = await _orderDbContext.OrderItems.Where(k => k.ProductId == context.Message.CourseId).ToListAsync();
            orderItems.ForEach(k =>
            {
                k.UpdateOrderItem(context.Message.UpdatedName, k.PictureUrl, k.Price);
            });

            await _orderDbContext.SaveChangesAsync();
        }
    }
}
