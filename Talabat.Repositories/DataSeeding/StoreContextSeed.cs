using System.Text.Json;
using Talabat.Core.Entities;
using Talabat.Repositories.Data;

namespace Talabat.Repositories.DataSeeding
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreDbContext _DbContext)
        {

            if (!_DbContext.Brands.Any())
            {
                var BrandsData = File.ReadAllText("../Talabat.Repositories/DataSeeding/DataSeed/brands.json");

                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                if (Brands?.Count() > 0)
                {
                    foreach (var Brand in Brands)
                    {
                        Brand.Id = 0;
                        _DbContext.Brands.Add(Brand);
                    }
                    _DbContext.SaveChanges();
                }
            }


            if (!_DbContext.Categories.Any())
            {
                var categoriesData = File.ReadAllText("../Talabat.Repositories/DataSeeding/DataSeed/categories.json");

                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);

                if (categories?.Count() > 0)
                {
                    foreach (var Brand in categories)
                    {
                        Brand.Id = 0;
                        _DbContext.Categories.Add(Brand);
                    }
                    _DbContext.SaveChanges();
                }
            }


            if (!_DbContext.Products.Any())
            {
                var productsData = File.ReadAllText("../Talabat.Repositories/DataSeeding/DataSeed/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count() > 0)
                {
                    foreach (var product in products)
                    {

                        _DbContext.Products.Add(product);
                    }
                    _DbContext.SaveChanges();
                }
            }


        }
    }
}
