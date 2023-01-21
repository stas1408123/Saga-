using MongoDB.Driver;
using WareHouse.OrderService.Domain.Contracts.Entities;

namespace WareHouse.OrderService.Application.Contracts.Contexts
{
    public interface IMongoDBContext
    {
        IMongoCollection<TDocument> GetCollection<TDocument>() where TDocument : IDocument;
    }
}
