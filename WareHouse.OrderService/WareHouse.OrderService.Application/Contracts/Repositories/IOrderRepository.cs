using WareHouse.OrderService.Domain.Entities;
using WareHouse.OrderService.Domain.Enums;

namespace WareHouse.OrderService.Application.Contracts.Repositories
{
    public interface IOrderRepository : IGenericRepository<OrderEntity>
    {
        Task<OrderEntity?> UpdateStatus(string id, OrderStatus status, CancellationToken cancellationToken);
    }
}
