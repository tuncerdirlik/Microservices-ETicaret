using FreeCourse.Shared.Dtos;
using FreeCourseServices.Order.Application.Dtos;
using FreeCourseServices.Order.Application.Mapping;
using FreeCourseServices.Order.Application.Queries;
using FreeCourseServices.Order.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCourseServices.Order.Application.Handlers
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;

        public GetOrdersByUserIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Include(k => k.OrderItems).Where(k => k.BuyerId.Equals(request.UserId)).ToListAsync();
            if (!orders.Any())
            {
                return Response<List<OrderDto>>.Success(new List<OrderDto>(), FreeCourse.Shared.Enums.ResponseStatusCodes.Ok);
            }

            var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

            return Response<List<OrderDto>>.Success(ordersDto, FreeCourse.Shared.Enums.ResponseStatusCodes.Ok);
        }
    }
}
