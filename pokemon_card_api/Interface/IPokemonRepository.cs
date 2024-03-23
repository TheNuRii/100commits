namespace pokemon_card_api.Interface;
using pokemon_card_api.Model;

public interface IPokemonRepository
{
    ICollection<Pokemon> GetPokemons();
}

