using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pokemon_card_api.Data;
using pokemon_card_api.Interface;
using pokemon_card_api.Model;

namespace pokemon_card_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : Controller
{
    private readonly IRewiewRepository _rewiewRepository;
    private readonly IMapper _mapper;
    public ReviewController(IRewiewRepository rewiewRepository, IMapper mapper)
    {
        _rewiewRepository = rewiewRepository;
        _mapper = mapper;
    }

    [HttpGet("rewiewId")]
    [ProducesResponseType(200, Type = typeof(Review))]
    [ProducesResponseType(400)]
    public IActionResult GetReview(int rewier)
    {
    }
}

