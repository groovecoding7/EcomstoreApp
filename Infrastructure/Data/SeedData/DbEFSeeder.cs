using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.RepositoriesContexts;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data.SeedData
{
    public class DbEFSeeder
    {

        public DbEFSeeder()
        {
        }

        public static void Seed(DbEntityFrameworkContext ctx)
        {
            DbEFSeeder seeder = new DbEFSeeder();

            seeder.PopulateProductBrandData(ctx);
            seeder.PopulateProductTypeData(ctx);
            seeder.PopulateProductData(ctx);
        }

        public void PopulateProductBrandData(DbEntityFrameworkContext ctx)
        {
            if (!ctx.ProductBrand.Any())
            {
                try
                {
                    var entities = JsonSerializer.Deserialize<List<ProductBrand>>(File.ReadAllText("../Infrastructure/Data/SeedData/brands.json"));
                    foreach (ProductBrand entity in entities)
                    {
                        ctx.ProductBrand.Add(entity);
                    }
                    ctx.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Logger.GetLogger("DbEFSeeder").LogError(ex, "Error occurred while seeding data.");
                }
            }
        }

        public void PopulateProductTypeData(DbEntityFrameworkContext ctx)
        {
            if (!ctx.ProductType.Any())
            {
                try
                {
                    var entities = JsonSerializer.Deserialize<List<ProductType>>(File.ReadAllText("../Infrastructure/Data/SeedData/types.json"));

                    foreach (ProductType entity in entities)
                    {
                        ctx.ProductType.Add(entity);
                    }

                    ctx.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Logger.GetLogger("DbEFSeeder").LogError(ex, "Error occurred while seeding data.");
                }
            }
        }

        public void PopulateProductData(DbEntityFrameworkContext ctx)
        {
            if (!ctx.Product.Any())
            {
                try
                {
                    var entities = JsonSerializer.Deserialize<List<Product>>(File.ReadAllText("../Infrastructure/Data/SeedData/products.json"));

                    foreach (Product entity in entities)
                    {
                        ctx.Product.Add(entity);
                    }

                    ctx.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Logger.GetLogger("DbEFSeeder").LogError(ex, "Error occurred while seeding data.");
                }

            }

        }

    }

}
