using Core.Entities;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public interface IPersistenceLayer
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> GetProduct(int id);
        public Task<Product> CreateProduct(int id, string name);
    }

    public class PersistenceLayer : IPersistenceLayer
    {
        private readonly IMemoryRepository _memoryRepository = null;
        private readonly IEfRepository _dbiEfRepository = null;
        private readonly IDaRepository _dbDaRepository = null;
        private string _databaseModes = null;
        private readonly ILogger _logger;
        
        public enum DatabaseModes
        {
            Memory,
            EF,
            DA
        } 

        public PersistenceLayer(
            ILoggerFactory loggerFactory,
            IConfiguration configuration,
            IMemoryRepository memoryRepository,
            IEfRepository dbiEfRepository,
            IDaRepository dbDaRepository
            )
        {
            _logger = loggerFactory.CreateLogger("PersistenceLayer");
            _databaseModes = configuration["DatabaseMode"];
            _memoryRepository = memoryRepository;
            _dbiEfRepository = dbiEfRepository;
            _dbDaRepository = dbDaRepository;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            ILogger logger = Logger.GetLogger("PersistenceLayer");

            var dbMode = Environment.GetEnvironmentVariable("DatabaseMode");

            logger.LogInformation($"Environment Variable DatabaseMode={dbMode}");
            logger.LogInformation($"AppSettings: DatabaseModes={_databaseModes.ToString()}");

            if (_databaseModes == "Memory" || _databaseModes == "DA")
                return await _memoryRepository.GetProducts();
            else 
                return await _dbiEfRepository.GetProducts();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _dbDaRepository.GetProduct(id);
        }

        public async Task<Product> CreateProduct(int id, string name)
        {
            return await _dbiEfRepository.CreateProduct(id,name);
        }
    }
}


