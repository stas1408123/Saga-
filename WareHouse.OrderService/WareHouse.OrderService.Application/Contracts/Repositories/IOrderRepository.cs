using WareHouse.OrderService.Domain.Entities;

namespace WareHouse.OrderService.Application.Contracts.Repositories
{
    public interface IOrderRepository : IGenericRepository<OrderEntity>
    {
    }
}
