using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace WareHouse.OrderService.Infrastructure.Options
{
    public class MongoDBOptionsSetup : IConfigureOptions<MongoDBOptions>
    {
        private readonly IConfiguration _configuration;

        public MongoDBOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(MongoDBOptions options)
        {
            _configuration.GetSection(MongoDBOptions.MongoSettings).Bind(options);
        }
    }
}
