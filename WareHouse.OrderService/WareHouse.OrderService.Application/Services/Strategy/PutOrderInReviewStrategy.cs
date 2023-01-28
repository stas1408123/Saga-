using AutoMapper;
using MassTransit;
using Warehouse.Contracts.DTOs;
using WareHouse.IntegrationEvents;
using WareHouse.OrderService.Application.Contracts.Repositories;
using WareHouse.OrderService.Application.Contracts.Strategy;
using WareHouse.OrderService.Application.Models;
using WareHouse.OrderService.Domain.Entities;
using WareHouse.OrderService.Domain.Enums;

namespace WareHouse.OrderService.Application.Services.Strategy
{
    public class PutOrderInReviewStrategy : IChangeOrderStatusStrategy
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public PutOrderInReviewStrategy(IOrderRepository orderRepository, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(orderRepository);
            ArgumentNullException.ThrowIfNull(publishEndpoint);
            ArgumentNullException.ThrowIfNull(mapper);

            _orderRepository = orderRepository;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }

        public async Task<Order> ChangeOrderStatus(string orderId, CancellationToken cancellationToken)
        {
            var entity = await _orderRepository.GetByPredicate(x => x.Id == orderId, cancellationToken);

            ValidateOrderInReview(entity.FirstOrDefault());

            var updatedEntity = await _orderRepository.UpdateStatus(orderId, OrderStatus.InReview, cancellationToken);
            var order = _mapper.Map<Order>(updatedEntity);

            await _publishEndpoint.Publish(new OrderInReviewIntegrationEvent(_mapper.Map<OrderDTO>(order)));

            return order;
        }

        public bool IsApplicable(OrderStatus status) => status is OrderStatus.InReview;

        private static void ValidateOrderInReview(OrderEntity? order)
        {
            if (order is null) throw new ArgumentNullException(nameof(order));

            if (order.OrderStatus is OrderStatus.InReview or OrderStatus.Declined or OrderStatus.Approved) throw new ArgumentException(nameof(order));
        }
    }
}
