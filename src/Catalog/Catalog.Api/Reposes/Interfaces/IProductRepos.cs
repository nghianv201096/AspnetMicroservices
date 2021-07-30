using Catalog.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Api.Reposes.Interfaces
{
    public interface IProductRepos
    {
        public Task<IEnumerable<Product>> Get();
        public Task<IEnumerable<Product>> GetByName(string name);
        public Task<IEnumerable<Product>> GetByCategory(string category);
        public Task<Product> GetById(string id);
        public Task<string> Insert(Product item);
        public Task<bool> Update(string id, Product item);
        public Task<bool> Delete(string id);
    }
}
