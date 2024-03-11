using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Infrastructure.Data;
using Core.Entities;
using Core.Specifications;
using API.Dtos;
using AutoMapper; // Add the namespace for Task



namespace API.Controllers
{
    [ApiController]
    //add route
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IGenericRepository<Product> _productsRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        public IMapper _mapper { get; }

        public ProductsController(ILogger<ProductsController> logger, IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo, IMapper mapper) // Modify constructor
        {
            _mapper= mapper;
            _logger = logger;
            _productsRepository = productsRepo; // Initialize _productsRepository field
            _productBrandRepository = productBrandRepo; // Initialize _productBrandRepository field
            _productTypeRepository = productTypeRepo; // Initialize _productTypeRepository field
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            _logger.LogInformation("Retrieving products");

            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productsRepository.ListAsync(spec); // Use the ListAsync method 
            var productsToReturn = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products); // Use the Map method
            return Ok(productsToReturn);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            _logger.LogInformation("Retrieving product with ID: {ProductId}", id);

            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productsRepository.GetEntityWithSpec(spec);
            if (product == null)
            {
                return NotFound();
            }

            var productToReturn = _mapper.Map<Product, ProductToReturnDto>(product);
            return Ok(productToReturn);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            _logger.LogInformation("Retrieving product brands");
            var productBrands = await _productBrandRepository.ListAllAsync(); 
            return Ok(productBrands); 
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            _logger.LogInformation("Retrieving product types");
            var productTypes = await _productTypeRepository.ListAllAsync(); 
            return Ok(productTypes); 
        }

    }
}
