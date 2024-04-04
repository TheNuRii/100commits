using pokemon_card_api.Data;
using pokemon_card_api.Interface;
using pokemon_card_api.Model;

namespace pokemon_card_api.Repository;

public class ReviewRepository : IRewiewRepository
{
    private readonly DataContext _context;
    public ReviewRepository(DataContext context)
    {
        _context = context;
    }
    public ICollection<Review> GetRewiews()
    {
        return _context.Rewiews.OrderBy(o => o.Id).ToList();
    }

    public Review GetRewiew(int reviewId)
    {
        return _context.Rewiews.Where(o => o.Id == reviewId).FirstOrDefault();
    }

    public ICollection<Review> GetReviewsOfAPokemon(int pokeId)
    {
        return _context.Rewiews.Where(p => p.Pokemon.Id == pokeId).ToList();
    }

    public bool RewiewExists(int reviewId)
    {
        return _context.Rewiews.Any(o => o.Id == reviewId);
    }
}

