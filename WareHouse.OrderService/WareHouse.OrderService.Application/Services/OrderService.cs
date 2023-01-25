using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Warehouse.Contracts.DTOs;
using WareHouse.IntegrationEvents;
using WareHouse.OrderService.Application.Contracts.Repositories;
using WareHouse.OrderService.Application.Contracts.Services;
using WareHouse.OrderService.Application.Models;
using WareHouse.OrderService.Domain.Entities;
using WareHouse.OrderService.Domain.Enums;

namespace WareHouse.OrderService.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IPublishEndpoint publishEndpoint, ILogger<OrderService> logger)
        {
            ArgumentNullException.ThrowIfNull(orderRepository);
            ArgumentNullException.ThrowIfNull(mapper);
            ArgumentNullException.ThrowIfNull(publishEndpoint);
            ArgumentNullException.ThrowIfNull(logger);

            _mapper = mapper;
            _orderRepository = orderRepository;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task<Order> PerformOrder(OrderDetails orderDetails, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start perform order process");

            var order = new Order()
            {
                Date = DateTime.UtcNow,
                OrderStatus = OrderStatus.Pending
            };

            _mapper.Map(orderDetails, order);

            var orderEntity = _mapper.Map<OrderEntity>(order);
            var insertedEntity = await _orderRepository.Insert(orderEntity, cancellationToken);
            var result = _mapper.Map<Order>(insertedEntity);

            await _publishEndpoint.Publish(new OrderStartedIntegrationEvent(_mapper.Map<OrderDTO>(result)));

            _logger.LogInformation("Perform order process: Published order started event. Order id: {order.Id}", result.Id);

            return order;
        }

        public async Task<Order> ChangeStatus(string id, OrderStatus status, CancellationToken cancellationToken)
        {
            var updatedEntity = await _orderRepository.UpdateStatus(id, status, cancellationToken);
            var result = _mapper.Map<Order>(updatedEntity);

            return result;
        }

        public async Task<IEnumerable<Order>> GetAll(CancellationToken cancellationToken)
        {
            var entities = await _orderRepository.Get(cancellationToken);
            var result = _mapper.Map<IEnumerable<Order>>(entities);

            return result;
        }

        public async Task<Order> GetById(string id, CancellationToken cancellationToken)
        {
            var entity = await _orderRepository.GetByPredicate(x => x.Id == id, cancellationToken);
            var result = _mapper.Map<Order>(entity);

            return result;
        }
    }
}
