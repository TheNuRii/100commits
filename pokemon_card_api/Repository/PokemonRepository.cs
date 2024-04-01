using AutoMapper;
using pokemon_card_api.Data;
using pokemon_card_api.Interface;
using pokemon_card_api.Model;

namespace pokemon_card_api.Repository;

public class PokemonRepository : IPokemonRepository 
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    
    public PokemonRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ICollection<Pokemon> GetPokemons()
    {
        return _context.Pokemons.OrderBy(p => p.Id).ToList();
    }

    public Pokemon GetPokemon(int id)
    {
        return _context.Pokemons.Where(p => p.Id == id).FirstOrDefault();
    }

    public Pokemon GetPokemon(string name)
    {
        return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
    }

    public decimal GetPokemonRating(int pokeId)
    {
        var rewiew = _context.Rewiews.Where(p => p.Pokemon.Id == pokeId);

        if (rewiew.Count() <= 0)
        {
            return 0;
        }
        else
        {
            return ((decimal)rewiew.Sum(r => r.Rating) / rewiew.Count());
        }
    }

    public bool PokemonExists(int pokeId)
    {
        return _context.Pokemons.Any(p => p.Id == pokeId);
    }
}

