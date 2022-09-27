using Domain.Entities;
using Domain.Interfaces;
using StackExchange.Redis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        public BasketRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = _redis.GetDatabase();
        }

        public Task<bool> DeleteBasketAsync(string UserName)
        {
            return _database.KeyDeleteAsync(UserName);
        }

        public async Task<UserBasket> GetBasketAsync(string userName)
        {
            var data = await _database.StringGetAsync(userName);
            if (data.IsNullOrEmpty)
            {
                return null;
            }
            try
            {
                return JsonConvert.DeserializeObject<UserBasket>(data);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<string> GetUsers()
        {
            var server = GetServer();
            var data = server.Keys();

            return data.Select(k => k.ToString());
        }

        public async Task<UserBasket> UpdateBasketAsync(UserBasket userBasket)
        {
            var created = await _database.StringSetAsync(userBasket.UserName, JsonConvert.SerializeObject(userBasket));
            if (!created)
            {
                return null;
            }
            return await GetBasketAsync(userBasket.UserName);
        }

        private IServer GetServer()
        {
            var endpoint = _redis.GetEndPoints();
            return _redis.GetServer(endpoint.First());
        }
    }
}
