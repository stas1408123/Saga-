using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WareHouse.OrderService.Domain.Contracts.Entities;

namespace WareHouse.OrderService.Domain.Entities.Base
{
    public class EntityBase : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }
    }
}
