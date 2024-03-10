using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Infrastructure.Data;
using Core.Entities; // Add the namespace for Task



namespace API.Controllers
{
    [ApiController]
    //add route
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductRepository _productsRepository; // Replace StoreContext with IProductsRepository

        public ProductsController(ILogger<ProductsController> logger, IProductRepository productsRepository) // Modify constructor
        {
            _logger = logger;
            _productsRepository = productsRepository; // Assign IProductsRepository
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            _logger.LogInformation("Retrieving products");
            var products = await _productsRepository.GetProductsAsync(); // Retrieve products asynchronously using IProductsRepository
            return Ok(products); // Return the products
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            _logger.LogInformation($"Retrieving product with ID: {id}");
            var product = _productsRepository.GetProductByIdAsync(id); // Retrieve product by ID from the IProductsRepository
            if (product == null)
            {
                return NotFound(); // Return 404 if product is not found
            }
            return Ok(product); // Return the product
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            _logger.LogInformation("Retrieving product brands");
            var productBrands = await _productsRepository.GetProductBrandsAsync(); // Retrieve product brands asynchronously using IProductsRepository
            return Ok(productBrands); // Wrap the product brands with Ok method
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            _logger.LogInformation("Retrieving product types");
            var productTypes = await _productsRepository.GetProductTypesAsync(); // Retrieve product types asynchronously using IProductsRepository
            return Ok(productTypes); // Wrap the product types with Ok method
        }

    }
}
