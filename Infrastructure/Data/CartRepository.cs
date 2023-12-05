using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class CartRepository : ICartRepository
    {
        private readonly IDatabase _database;

        public CartRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<CustomerCart> GetCartAsync(string id)
        {
            var cart = await _database.StringGetAsync(id);
            return cart.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerCart>(cart);
        }

        public async Task<CustomerCart> UpdateCartAsync(CustomerCart cart)
        {
            bool created = await _database.StringSetAsync(cart.Id, JsonSerializer.Serialize(cart), TimeSpan.FromDays(30));
            if (!created) return null;
            return await GetCartAsync(cart.Id);
        }
        public async Task<bool> DeleteCartAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }
    }
}
