using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WareHouse.OrderService.Application.Contracts.Contexts;
using WareHouse.OrderService.Domain.Attributes;
using WareHouse.OrderService.Domain.Contracts.Entities;
using WareHouse.OrderService.Infrastructure.Persistence.Options;

namespace WareHouse.OrderService.Infrastructure.Persistence.Contexts
{
    public class MongoDBContext : IMongoDBContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoDBOptions _options;

        public MongoDBContext(IOptions<MongoDBOptions> options)
        {
            _options = options.Value;

            var client = new MongoClient(_options.ConnectionString);
            _database = client.GetDatabase(_options.DatabaseName);
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>() where TDocument : IDocument
        {
            return _database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        public static string? GetCollectionName(Type documentType)
        {
            return (documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true)
                                .FirstOrDefault() as BsonCollectionAttribute)?.CollectionName;
        }
    }
}
