namespace WareHouse.OrderService.Infrastructure.Persistence.Options
{
    public class MongoDBOptions
    {
        public const string MongoSettings = "MongoDbSettings";
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
    }
}
