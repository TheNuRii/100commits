using pokemon_card_api.Data;
using pokemon_card_api.Interface;
using pokemon_card_api.Model;

namespace pokemon_card_api.Repository;

public class PokemonRepository : IPokemonRepository
{
    private readonly DataContext _context;
    
    public PokemonRepository(DataContext context)
    {
        _context = context;
    }

    public ICollection<Pokemon> GetPokemons()
    {
        return _context.Pokemons.OrderBy(p => p.Id).ToList();
    }
}

