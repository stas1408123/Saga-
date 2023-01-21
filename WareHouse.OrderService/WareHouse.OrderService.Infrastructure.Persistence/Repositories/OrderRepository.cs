using WareHouse.OrderService.Application.Contracts.Contexts;
using WareHouse.OrderService.Application.Contracts.Repositories;
using WareHouse.OrderService.Domain.Entities;

namespace WareHouse.OrderService.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<OrderEntity>, IOrderRepository
    {
        public OrderRepository(IMongoDBContext context) : base(context) { }
    }
}
