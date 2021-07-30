using Basket.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.Api.Reposes.Interfaces
{
    public interface IBasketRepos
    {
        public Task<ShoppingCart> Get(string userName);
        public Task<bool> Save(ShoppingCart item);
        public Task<bool> Delete(string userName);
    }
}
