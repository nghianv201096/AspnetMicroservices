using Catalog.Api.Datas.Interfaces;
using Catalog.Api.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Api.Datas
{
    public class CatalogContextSeed
    {
        public static void SeedData(ICatalogContext context)
        {
            var haveData = context.Products
                .FindSync(FilterDefinition<Product>.Empty)
                .Any();

            if (!haveData)
            {
                context.Products
                    .InsertMany(GetSeedingProduct());
            }
        }

        private static IEnumerable<Product> GetSeedingProduct()
        {
            return new List<Product> {
                new Product()
                {
                    Id = "5da863bb34e44957b4505004",
                    Name = "Asus Nitro",
                    Category = "Computer",
                    Summary = "Newest computer",
                    Description = "I7 gen10, 8gb Ram",
                    Price = 699.99m
                },
                new Product()
                {
                    Id = "5da863bb34e44957b4505005",
                    Name = "Macbook Pro 2021",
                    Category = "Computer",
                    Summary = "Newest computer",
                    Description = "I7 gen10, 8gb Ram",
                    Price = 2699.99m
                },
                new Product()
                {
                    Id = "5da863bb34e44957b4505006",
                    Name = "Iphone 14",
                    Category = "Phone",
                    Summary = "Newest phone",
                    Description = "A14",
                    Price = 1699.99m
                },
                new Product()
                {
                    Id = "5da863bb34e44957b4505007",
                    Name = "Redmi 9",
                    Category = "Phone",
                    Summary = "Newest phone",
                    Description = "China",
                    Price = 199.99m
                }
            };
        }
    }
}
