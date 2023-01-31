﻿using AutoMapper;
using WareHouse.OrderService.Application.Contracts.Repositories;
using WareHouse.OrderService.Application.Contracts.Strategy;
using WareHouse.OrderService.Application.Models;
using WareHouse.OrderService.Domain.Entities;
using WareHouse.OrderService.Domain.Enums;

namespace WareHouse.OrderService.Application.Services.Strategy
{
    public class MarkOrderAsFailedStrategy : IChangeOrderStatusStrategy
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public MarkOrderAsFailedStrategy(IOrderRepository orderRepository, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(orderRepository);
            ArgumentNullException.ThrowIfNull(mapper);

            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<Order> ChangeOrderStatus(string orderId, CancellationToken cancellationToken)
        {
            var entity = await _orderRepository.GetByPredicate(x => x.Id == orderId, cancellationToken);

            ValidateOrderApproval(entity.FirstOrDefault());

            var updatedEntity = await _orderRepository.UpdateStatus(orderId, OrderStatus.Failed, cancellationToken);
            var order = _mapper.Map<Order>(updatedEntity);

            return order;
        }

        public bool IsApplicable(OrderStatus status) => status is OrderStatus.Failed;

        private static void ValidateOrderApproval(OrderEntity? order)
        {
            if (order is null) throw new ArgumentNullException(nameof(order));

            if (order.OrderStatus == OrderStatus.Failed) throw new ArgumentException(nameof(order));
        }
    }
}
