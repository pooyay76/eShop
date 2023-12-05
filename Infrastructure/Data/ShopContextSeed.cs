using Core.Entities;
using System.Text.Json;

namespace Infrastructure.Data
{
    public static class ShopContextSeed
    {
        public static async Task SeedAsync(ShopContext shopContext)
        {
            var seedPath = "../Infrastructure/Data/DataSeed";

            if (shopContext.Products.Any() == false)
            {
                var productSeedPath = seedPath + "/products.json";
                var productData = ReadDataFromJsonFile<List<Product>>(productSeedPath);
                await shopContext.Products.AddRangeAsync(productData);
            }

            if (shopContext.ProductBrands.Any() == false)
            {

                var brandSeedPath = seedPath + "/brands.json";
                var brandData = ReadDataFromJsonFile<List<ProductBrand>>(brandSeedPath);
                await shopContext.ProductBrands.AddRangeAsync(brandData);
            }

            if (shopContext.ProductTypes.Any() == false)
            {
                var typeSeedPath = seedPath + "/types.json";
                var typeData = ReadDataFromJsonFile<List<ProductType>>(typeSeedPath);
                await shopContext.ProductTypes.AddRangeAsync(typeData);
            }
            if (shopContext.ChangeTracker.HasChanges())
                await shopContext.SaveChangesAsync();
        }
        private static T ReadDataFromJsonFile<T>(string filePath)
        {
            var text = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(text);
        }
    }
}
