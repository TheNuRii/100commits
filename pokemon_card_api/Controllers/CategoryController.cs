using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using pokemon_card_api.Dto;
using pokemon_card_api.Interface;
using pokemon_card_api.Model;

namespace pokemon_card_api.Controllers;

[Route("api/[Controller]")]
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
    public IActionResult GetCategory(int categoryId)
    {
        if (!_categoryRepository.CategoryExist(categoryId))
            return NotFound();

        var category = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategory(categoryId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(category);
    }
}

