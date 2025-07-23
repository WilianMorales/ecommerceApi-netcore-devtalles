using AutoMapper;
using ecommerceApi_netcore_devtalles.Models;
using ecommerceApi_netcore_devtalles.Models.Dtos;
using ecommerceApi_netcore_devtalles.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ecommerceApi_netcore_devtalles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetAllProducts();
            if (products == null || !products.Any())
            {
                return NotFound("No se encontraron productos.");
            }

            var productsDto = _mapper.Map<List<ProductDto>>(products);
            return Ok(productsDto);
        }

        [HttpGet("{productId:int}", Name = "GetProductById")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetProductById(int productId)
        {
            var product = _productRepository.GetProductById(productId);
            if (product == null || product.ProductId == 0)
            {
                return NotFound($"No se encontró el producto con ID {productId}.");
            }

            var productDto = _mapper.Map<ProductDto>(product);
            if (productDto == null)
            {
                return NotFound($"No se pudo mapear el producto con ID {productId}.");
            }
            return Ok(productDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            if (createProductDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_productRepository.ProductExists(createProductDto.Name))
            {
                ModelState.AddModelError("CustomError", "El producto ya existe.");
                return BadRequest(ModelState);
            }

            if (!_categoryRepository.CategoryExists(createProductDto.CategoryId))
            {
                ModelState.AddModelError("CustomError", "La categoría especificada no existe.");
                return BadRequest(ModelState);
            }

            var product = _mapper.Map<Product>(createProductDto);
            if (!_productRepository.CreateProduct(product))
            {
                ModelState.AddModelError("CustomError", $"Error al guardar el registro del producto {product.Name}.");
                return StatusCode(500, ModelState);
            }

            var createProduct = _productRepository.GetProductById(product.ProductId);
            var productDto = _mapper.Map<ProductDto>(createProduct);
            return CreatedAtRoute("GetProductById", new { productId = product.ProductId }, productDto);
        }
    }
}
