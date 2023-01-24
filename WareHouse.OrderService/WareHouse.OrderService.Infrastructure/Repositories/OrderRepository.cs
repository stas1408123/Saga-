using MongoDB.Driver;
using WareHouse.OrderService.Application.Contracts.Contexts;
using WareHouse.OrderService.Application.Contracts.Repositories;
using WareHouse.OrderService.Domain.Entities;
using WareHouse.OrderService.Domain.Enums;

namespace WareHouse.OrderService.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<OrderEntity>, IOrderRepository
    {
        public OrderRepository(IMongoDBContext context) : base(context) { }

        public async Task<OrderEntity?> UpdateStatus(string id, OrderStatus status, CancellationToken cancellationToken)
        {
            var entity = await _collection.FindOneAndUpdateAsync(x => x.Id == id, Builders<OrderEntity>.Update.Set(rec => rec.OrderStatus, status), cancellationToken: cancellationToken);

            return entity;
        }
    }
}
