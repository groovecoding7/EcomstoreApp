using System.Diagnostics;
using Core.Entities;
using Core.Specifications;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IGenericPersistenceLayer<Product> _genericPersistenceLayer;
        private readonly ILogger _logger;

        public ProductsController(ILoggerFactory loggerFactory, IGenericPersistenceLayer<Product> productsRepository)
        {
            _logger = loggerFactory.CreateLogger("ProductsController");
         
            _genericPersistenceLayer = productsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                _logger.LogInformation("Invoking the GetProducts API Function.");

                var products = await _genericPersistenceLayer.GetEntities();
                
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error occurred while GetProducts API.");
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducts(int id)
        {
            try
            {
                _logger.LogInformation($"Invoking the GetProducts By ID API Function with Id={id}");

                var spec = new ProductWithTypeAndBrandSpecification();

                var product = await _genericPersistenceLayer.GetEntity(spec);

                return Ok(product);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(int id, string name)
        {
            try
            {
                _logger.LogInformation($"Invoking the CreateProducts By ID API Function with Id={id}, Name={name}");

                Product product = new Product() {Id = id, Name = name};
                
                await _genericPersistenceLayer.CreateEntity(product);

                return Ok(product);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}