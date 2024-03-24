using Microsoft.AspNetCore.Mvc;
using pokemon_card_api.Interface;
using pokemon_card_api.Model;

namespace pokemon_card_api.Controllers;

[Route("api/[Controller]")]
[ApiController]

public class PokemonController : Controller
{
    private readonly IPokemonRepository _pokemonRepository;
    
    public PokemonController(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
    public IActionResult GetPokemons()
    {
        var pokemons = _pokemonRepository.GetPokemons();

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(pokemons);
    }
}


