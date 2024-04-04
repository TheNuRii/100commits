using pokemon_card_api.Model;
namespace pokemon_card_api.Interface;

public interface IRewiewRepository
{
    ICollection<Review> GetRewiews();
    Review GetRewiew(int reviewId);
    ICollection<Review> GetReviewsOfAPokemon(int pokeId);
    bool RewiewExists(int reviewId);
}

