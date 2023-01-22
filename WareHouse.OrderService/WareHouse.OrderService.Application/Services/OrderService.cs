using WareHouse.OrderService.Application.Contracts.Repositories;
using WareHouse.OrderService.Application.Contracts.Services;
using WareHouse.OrderService.Application.Models;

namespace WareHouse.OrderService.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<Order> ChangeStatus(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Order> Create(Order order, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAll(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetById(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
