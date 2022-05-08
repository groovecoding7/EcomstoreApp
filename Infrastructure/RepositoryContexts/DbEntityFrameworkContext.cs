using System.Reflection;
using Core.Entities;
using Infrastructure.Data.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.RepositoriesContexts
{
    public partial class DbEntityFrameworkContext : DbContext
    {
        public DbEntityFrameworkContext(DbContextOptions<DbEntityFrameworkContext> options) : base(options)
        {
        }
        public DbSet<ProductBrand> ProductBrand { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Product> Product { get; set; }

        /// <summary>
        /// We need to override this method to point to our 'Data/Config' changes so that they
        /// are picked up when the EF creates the new Migrations.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public static void OnStartUp(IServiceCollection services)
        {
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                try
                {
                    var ctx = serviceProvider.GetRequiredService<DbEntityFrameworkContext>();
                    ctx.Database.MigrateAsync();
                    DbEFSeeder.Seed(ctx);
                }
                catch(Exception ex)
                {
                    Logger.GetLogger("Migrations").LogError(ex, "An error occurred during database migration.");
                }
            }
        }
    }
}