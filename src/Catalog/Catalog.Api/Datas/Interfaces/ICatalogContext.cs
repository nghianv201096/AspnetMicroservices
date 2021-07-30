using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Datas.Interfaces
{
    public interface ICatalogContext
    {
        public IMongoCollection<Product> Products { get; set; }
    }
}
