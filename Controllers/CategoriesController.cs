using AutoMapper;
using ecommerceApi_netcore_devtalles.Models.Dtos;
using ecommerceApi_netcore_devtalles.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ecommerceApi_netcore_devtalles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetAllCategories();
            if (categories == null || !categories.Any())
            {
                return NotFound("No se encontraron categorías.");
            }

            var categoriesDto = new List<CategoryDto>();
            foreach (var category in categories)
            {
                categoriesDto.Add(_mapper.Map<CategoryDto>(category));
            }
            return Ok(categoriesDto);
        }

        [HttpGet("{id:int}", Name = "GetCategoryById")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategoryById(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if (category == null || category.Id == 0)
            {
                return NotFound($"No se encontró la categoría con ID {id}.");
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);
            if (categoryDto == null)
            {
                return NotFound($"No se pudo mapear la categoría con ID {id}.");
            }
            return Ok(categoryDto);
        }
    }
}
