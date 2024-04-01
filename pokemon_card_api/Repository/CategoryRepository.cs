using AutoMapper;
using pokemon_card_api.Data;
using pokemon_card_api.Interface;
using pokemon_card_api.Model;

namespace pokemon_card_api.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public CategoryRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public ICollection<Category> GetCategories()
    {
        return _context.Categories.ToList();
    }

    public Category GetCategory(int id)
    {
        return _context.Categories.Where(e => e.Id == id).FirstOrDefault();
    }

    public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
    {
        return _context.PokemonCategories.Where(e => e.CategoryId == categoryId).Select(c => c.Pokemon).ToList();
     }

    public bool CategoryExist(int id)
    {
        return _context.Categories.Any(c => c.Id == id);
    }
}

