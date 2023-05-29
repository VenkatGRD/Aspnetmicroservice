using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Net.Http.Json;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using System;

namespace Basket.API.Repositories
{
    public class BasketRepository:IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache radisCache)
        {
            _redisCache = radisCache;
        }
        public async Task<ShoppingCart> GetBasket(string userName)
        {
            try
            {
                var basket = await _redisCache.GetStringAsync(userName);
                if (basket == null)
                {
                    return null;
                }
                else
                {
                    return JsonConvert.DeserializeObject<ShoppingCart>(basket);

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));

            return await GetBasket(basket.UserName);
        }

        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
    }
}
