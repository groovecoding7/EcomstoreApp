using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Core.Entities;
using Core.Specifications;
using Infrastructure.RepositoriesContexts;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    
    public interface IEfRepository 
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> CreateProduct(int id, string name);
    }

    public class EntityFrameworkRepository : IEfRepository
    {

        private readonly DbEntityFrameworkContext _dbContext = null;
       
        public EntityFrameworkRepository(DbEntityFrameworkContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
           return await _dbContext.Product.ToListAsync();
        }

        public async Task<Product> CreateProduct(int id, string name)
        {

            if (!_dbContext.Product.Any(p => p.Id == id))
            {
                Product product = new Product() { Id = id, Name = name };

                _dbContext.Add(product);

                await _dbContext.SaveChangesAsync();

                return product;
            }
            else
            {
                throw new Exception($"The product with Id={id} already exists.");
            }
        }
    }
}
