namespace WareHouse.OrderService.Infrastructure.Options
{
    public class MongoDBOptions
    {
        public const string MongoSettings = "MongoDbSettings";
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
