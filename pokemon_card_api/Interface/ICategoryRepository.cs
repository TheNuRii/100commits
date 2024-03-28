using pokemon_card_api.Model;

namespace pokemon_card_api.Interface;

public interface ICategoryRepository
{
    ICollection<Category> GetCategories();
    Category GetCategory(int id);
    ICollection<Pokemon> GetPokemonByCategory(int categoryId);
    bool CategoryExist(int id);
}

