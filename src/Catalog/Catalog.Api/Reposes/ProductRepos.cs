using Catalog.Api.Datas.Interfaces;
using Catalog.Api.Entities;
using Catalog.Api.Reposes.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Api.Reposes
{
    public class ProductRepos : IProductRepos
    {
        private readonly ICatalogContext _context;

        public ProductRepos(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> Get()
        {
            var items = (await _context.Products
                .FindAsync(FilterDefinition<Product>.Empty))
                .ToList();

            return items;
        }

        public async Task<IEnumerable<Product>> GetByCategory(string category)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Category, category);
            var items = await _context.Products
                .Find(filter)
                .ToListAsync();

            return items;
        }

        public async Task<IEnumerable<Product>> GetByName(string name)
        {
            var filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
            var items = await _context.Products
                .Find(filter)
                .ToListAsync();

            return items;
        }

        public async Task<Product> GetById(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var item = await _context.Products
                .Find(filter)
                .FirstOrDefaultAsync();

            return item;
        }

        public async Task<string> Insert(Product item)
        {
            await _context.Products.InsertOneAsync(item);

            return item.Id;
        }

        public async Task<bool> Update(string id, Product item)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var rs = await _context.Products.ReplaceOneAsync(filter, item);

            return rs.IsAcknowledged
                && rs.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var rs = await _context.Products.DeleteOneAsync(filter);

            return rs.IsAcknowledged
                && rs.DeletedCount > 0;
        }
    }
}
