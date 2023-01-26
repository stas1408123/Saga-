using AutoMapper;
using Warehouse.Contracts.DTOs;
using WareHouse.IntegrationEvents;
using WareHouse.OrderService.Application.Contracts.Factories;
using WareHouse.OrderService.Application.Models;
using WareHouse.OrderService.Domain.Enums;

namespace WareHouse.OrderService.Application.Factories
{
    public class OrderEventsFactory : IOrderEventsFactory
    {
        private readonly IMapper _mapper;

        public OrderEventsFactory(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IntegrationEvent<OrderDTO> CreateIntegrationEvent(OrderStatus status, Order order)
        {
            var orderDto = _mapper.Map<OrderDTO>(order);

            IntegrationEvent<OrderDTO> integrationEvent = status switch
            {
                OrderStatus.Approved => new OrderApprovedIntegrationEvent(orderDto),
                OrderStatus.InReview => new OrderInReviewIntegrationEvent(orderDto),
                OrderStatus.Declined => new OrderDeclinedIntegrationEvent(orderDto),
                OrderStatus.Pending => new OrderIsPendingIntegrationEvent(orderDto),
                OrderStatus.None => throw new ArgumentException("Order status None is invalid to set."),
            };

            return integrationEvent;
        }
    }
}
