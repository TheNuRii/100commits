namespace pokemon_card_api.Model;

public class Pokemon
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public ICollection<Review> Rewiews { get; set;}
    public ICollection<PokemonCategory> PokemonCategories { get; set; }
    public ICollection<PokemonOwner> PokemonOwners { get; set; }
}

