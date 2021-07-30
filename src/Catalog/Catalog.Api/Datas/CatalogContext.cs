using Catalog.Api.Datas.Interfaces;
using Catalog.Api.Entities;
using Catalog.Api.Utilities.Constants;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Api.Datas
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration config)
        {
            Setup(config);
            CatalogContextSeed.SeedData(this);
        }

        public IMongoCollection<Product> Products { get; set; }

        private void Setup(IConfiguration config) {
            var db = GetDatabase(config);

            Products = GetCollection<Product>(db, Constants.COLLECTION_PRODUCT);
        }

        private IMongoDatabase GetDatabase(IConfiguration config) {
            var client = new MongoClient(config.GetValue<string>(Constants.CATALOG_DB_CONN_STRING));
            var db = client.GetDatabase(Constants.CATALOG_DB);
            
            return db;
        }

        private IMongoCollection<T> GetCollection<T>(IMongoDatabase db, string name)
        {
            return db.GetCollection<T>(name);
        }
    }
}
