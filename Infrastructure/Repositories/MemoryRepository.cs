using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Dapper;
using Infrastructure.RepositoriesContexts;
using Infrastructure.RepositoryContexts;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
   
    public interface IMemoryRepository
    {
        public Task<IEnumerable<Product>> GetProducts();
    }

    public class MemoryRepository : IMemoryRepository
    {
        private readonly IConfiguration _configuration;
        private readonly MemoryContext _memoryContext;
        private readonly IEnumerable<Product> _data;

        public MemoryRepository(MemoryContext context, IConfiguration configuration)
        {
            _memoryContext = context;
            _configuration = configuration;
            _data = new List<Product>()
            {
                new Product() {Id = 1, Name = "Product 1"},
                new Product() {Id = 2, Name = "Product 2"},
                new Product() {Id = 3, Name = "Product 3"},
                new Product() {Id = 4, Name = "Product 4"},
                new Product() {Id = 5, Name = "Product 5"},
                new Product() {Id = 6, Name = "Product 6"},
                new Product() {Id = 7, Name = "Product 7"},
                new Product() {Id = 8, Name = "Product 8"},
                new Product() {Id = 9, Name = "Product 9"},
                new Product() {Id = 10, Name = "Product 10"},
                new Product() {Id = 11, Name = "Product 11"},
            };
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await Task.FromResult(_data);
        }
    }
}
