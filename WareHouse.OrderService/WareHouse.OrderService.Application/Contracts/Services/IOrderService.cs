using WareHouse.OrderService.Application.Models;

namespace WareHouse.OrderService.Application.Contracts.Services
{
    public interface IOrderService
    {
        public Task<Order> Create(Order order, CancellationToken cancellationToken);
        public Task<Order> GetById(int id, CancellationToken cancellationToken);
        public Task<Order> ChangeStatus(CancellationToken cancellationToken);
        public Task<IEnumerable<Order>> GetAll(CancellationToken cancellationToken);
    }
}
