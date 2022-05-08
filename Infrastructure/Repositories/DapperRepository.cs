using Core.Entities;
using Dapper;
using Infrastructure.RepositoriesContexts;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public interface IDaRepository
    {
        public Task<Product> GetProduct(int id);
    }

    public class DapperRepository : IDaRepository
    {

        private readonly DbDapperContext _dbContext = null;

        public DapperRepository(DbDapperContext context, IConfiguration configuration)
        {
            _dbContext = context;
        }

        public async Task<Product> GetProduct(int id)
        {
            var query = $"SELECT * FROM Products WHERE Id = {id}";
            using (var connection = _dbContext?.CreateConnection())
            {
                return await connection.QuerySingleAsync<Product>(query);
            }
        }
    }
}

