using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            // Check if the ProductBrands table is empty
            if (!context.ProductBrands.Any())
            {
                // Read the brands data from the JSON file
                var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                // Deserialize the JSON data into a list of ProductBrand objects
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                // Add the brands to the ProductBrands table
                context.ProductBrands.AddRange(brands);
            }

            // Check if the ProductTypes table is empty
            if (!context.ProductTypes.Any())
            {
                // Read the types data from the JSON file
                var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                // Deserialize the JSON data into a list of ProductType objects
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                // Add the types to the ProductTypes table
                context.ProductTypes.AddRange(types);
            }

            // Check if the Products table is empty
            if (!context.Products.Any())
            {
                // Read the products data from the JSON file
                var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                // Deserialize the JSON data into a list of Product objects
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                // Add the products to the Products table
                context.Products.AddRange(products);
            }

            // Save the changes to the database
            if (context.ChangeTracker.HasChanges())
            {
                await context.SaveChangesAsync();
            }
            
        }
        
    }
}