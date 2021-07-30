using Basket.Api.Entities;
using Basket.Api.Reposes.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.Api.Reposes
{
    public class BasketRepos : IBasketRepos
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepos(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task<ShoppingCart> Get(string userName)
        {
            var item = await _redisCache.GetStringAsync(userName);
            if (item == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ShoppingCart>(item);
        }

        public async Task<bool> Save(ShoppingCart item)
        {
            try
            {
                var serializedItem = JsonConvert.SerializeObject(item);
                await _redisCache.SetStringAsync(item.UserName, serializedItem);

                return true;
            }
            catch
            {
                throw;
            }

        }

        public async Task<bool> Delete(string userName)
        {
            try
            {
                await _redisCache.RemoveAsync(userName);
                
                return true;
            }
            catch
            {
                throw;
            }

        }
    }
}
