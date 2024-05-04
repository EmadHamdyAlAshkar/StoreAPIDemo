using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Sevrice.Services.CashService
{
    public class CashService : ICashService
    {
        private readonly IDatabase _database;
        public CashService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<string> GeCashResponseAsync(string key)
        {
            var cashedResponse = await _database.StringGetAsync(key);

            if (cashedResponse.IsNullOrEmpty)
                return null;

            return cashedResponse.ToString();
        }

        public async Task SetCashResponseAsync(string key, object response, TimeSpan timeToLive)
        {
            if (response is null)
                return;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var serializedResponse = JsonSerializer.Serialize(response, options);

            await _database.StringSetAsync(key, serializedResponse, timeToLive);

        }
    }
}
