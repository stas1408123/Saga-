using WareHouse.OrderService.Application.Models;
using WareHouse.OrderService.Domain.Enums;

namespace WareHouse.OrderService.Application.Contracts.Services
{
    public interface IOrderService
    {
        public Task<Order> PerformOrder(OrderDetails orderDetails, CancellationToken cancellationToken);
        public Task<Order> GetById(string id, CancellationToken cancellationToken);
        public Task<Order> ChangeStatus(string id, OrderStatus status, CancellationToken cancellationToken);
        public Task<IEnumerable<Order>> GetAll(CancellationToken cancellationToken);
    }
}
