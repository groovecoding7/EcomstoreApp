using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Specifications;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{

    public interface IGenericPersistenceLayer<TEntity> where TEntity : BaseEntity
    {
        public Task<IReadOnlyList<TEntity>> GetEntities(ISpecification<TEntity> spec);
        public Task<TEntity> GetEntity(ISpecification<TEntity> spec);
        public Task<IReadOnlyList<TEntity>> GetEntities();
        public Task<TEntity> CreateEntity(TEntity entity);

    }
    
    public class GenericPersistentLayer<TEntity> : IGenericPersistenceLayer<TEntity> where TEntity : BaseEntity
    {
        private readonly IGenericRepository<TEntity> _genericRepository = null;
        private readonly string _databaseModes = null;
        private readonly ILogger _logger;

        public enum DatabaseModes
        {
            Memory,
            GenericRepository
        }

        public GenericPersistentLayer(
            ILoggerFactory loggerFactory,
            IConfiguration configuration,
            IGenericRepository<TEntity> genericRepository
            )
        {
            _logger = loggerFactory.CreateLogger("PersistenceLayer");
            _databaseModes = configuration["DatabaseMode"];
            _genericRepository = genericRepository;
        }

        public Task<IReadOnlyList<TEntity>> GetEntities(ISpecification<TEntity> spec)
        {
            return _genericRepository.ListAsync(spec);
        }

        public Task<TEntity> GetEntity(ISpecification<TEntity> spec)
        {
            return _genericRepository.GetEntityWithSpec(spec);
        }

        public Task<TEntity> CreateEntity(TEntity entity)
        {
            return _genericRepository.CreateEntity(entity);
        }

        public Task<IReadOnlyList<TEntity>> GetEntities()
        {
            return _genericRepository.ListAllAsync();
        }

    }
    
}
