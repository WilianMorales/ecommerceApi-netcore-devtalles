using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
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
    }
}
