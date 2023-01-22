using AutoMapper;
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

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<Order> PerformOrder(OrderDetails orderDetails, CancellationToken cancellationToken)
        {
            var order = new Order()
            {
                Date = DateTime.UtcNow,
                OrderStatus = OrderStatus.Pending
            };

            _mapper.Map(orderDetails, order);

            var orderEntity = _mapper.Map<OrderEntity>(order);

            var insertedEntity = await _orderRepository.Insert(orderEntity, cancellationToken);
            var result = _mapper.Map<Order>(insertedEntity);

            return result;
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
