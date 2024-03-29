using pokemon_card_api.Data;
using pokemon_card_api.Interface;
using pokemon_card_api.Model;

namespace pokemon_card_api.Repository;

public class CategoryRepository : ICategoryRepository
{
    private DataContext _context;
    public CategoryRepository(DataContext context)
    {
        _context = context;
    }
    public ICollection<Category> GetCategories()
    {
        throw new NotImplementedException();
    }

    public Category GetCategory(int id)
    {
        throw new NotImplementedException();
    }

    public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
    {
        throw new NotImplementedException();
    }

    public bool CategoryExist(int id)
    {
        return _context.Categories.Any(c => c.Id == id);
    }
}

