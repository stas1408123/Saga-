using MongoDB.Bson.Serialization.Attributes;
using WareHouse.OrderService.Domain.Contracts.Entities;

namespace WareHouse.OrderService.Domain.Entities.Base
{
    public class EntityBase : IDocument
    {
        [BsonId]
        public int Id { get; set; }
    }
}
