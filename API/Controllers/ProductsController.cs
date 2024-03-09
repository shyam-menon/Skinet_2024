using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Infrastructure.Data; // Add the namespace for Task



namespace API.Controllers
{
    //add route
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly StoreContext _context; // Add StoreContext

        public ProductsController(ILogger<ProductsController> logger, StoreContext context) // Modify constructor
        {
            _logger = logger;
            _context = context; // Assign StoreContext
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            _logger.LogInformation("Retrieving products");
            var products = await _context.Products.ToListAsync(); // Retrieve products asynchronously
            return Ok(products); // Return the products
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            _logger.LogInformation($"Retrieving product with ID: {id}");
            var product = _context.Products.Find(id); // Retrieve product by ID from the database
            if (product == null)
            {
                return NotFound(); // Return 404 if product is not found
            }
            return Ok(product); // Return the product
        }
    }
}
