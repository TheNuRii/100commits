using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using pokemon_card_api.Dto;
using pokemon_card_api.Interface;
using pokemon_card_api.Model;

namespace pokemon_card_api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [HttpGet("{categoryId}")]
    [ProducesResponseType(200, Type = typeof(Category))]
    [ProducesResponseType(400)]
    public IActionResult GetCategorie(int categoryId)
    {
        if (!_categoryRepository.CategoryExist(categoryId))
            return NotFound();

        var category = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategory(categoryId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(category);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
    
    public IActionResult GetCategorys()
    {
        var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(categories);
    }

    [HttpGet("pokemon/{categoryId}")]
    [ProducesResponseType(200, Type = typeof(int))]
    [ProducesResponseType(400)]
    public IActionResult GetPokemonByCategory(int categoryId)
    {
        if (!_categoryRepository.CategoryExist(categoryId))
            return NotFound();

        var category = _categoryRepository.GetPokemonByCategory(categoryId);

        if (!ModelState.IsValid)
            return BadRequest();

        return Ok(category);
    }
}

