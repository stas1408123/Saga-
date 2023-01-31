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
    public class DeclineOrderStrategy : IChangeOrderStatusStrategy
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public DeclineOrderStrategy(IOrderRepository orderRepository, IPublishEndpoint publishEndpoint, IMapper mapper)
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

            ValidateOrderDeclined(entity.FirstOrDefault());

            var updatedEntity = await _orderRepository.UpdateStatus(orderId, OrderStatus.Declined, cancellationToken);
            var order = _mapper.Map<Order>(updatedEntity);

            await _publishEndpoint.Publish(new OrderDeclinedIntegrationEvent(_mapper.Map<OrderDTO>(order)));

            return order;
        }

        public bool IsApplicable(OrderStatus status) => status is OrderStatus.Declined;

        private static void ValidateOrderDeclined(OrderEntity? order)
        {
            if (order is null) throw new ArgumentNullException(nameof(order));

            if (order.OrderStatus == OrderStatus.Declined || order.OrderStatus == OrderStatus.Failed) throw new ArgumentException(nameof(order));
        }

        private void ValidateOrderApproval(OrderEntity? order)
        {
            if (order is null) throw new ArgumentNullException(nameof(order));

            if (order.OrderStatus == OrderStatus.Declined) throw new ArgumentException(nameof(order));
        }
    }
}
