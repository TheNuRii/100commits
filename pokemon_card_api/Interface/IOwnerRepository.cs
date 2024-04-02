using pokemon_card_api.Model;

namespace pokemon_card_api.Interface;

public interface IOwnerRepository
{
    ICollection<Owner> GetOwners();
    Owner GetOwner(int ownerId);
    ICollection<Owner> GetOwnerOfAPokemon(int pokeId);
    ICollection<Pokemon> GetPokemonByOwner(int ownerId);
    bool OwnerExists(int ownerId);
}

