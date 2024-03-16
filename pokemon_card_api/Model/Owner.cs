namespace pokemon_card_api.Model;

public class Owner
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Gym { get; set; }
    public Country Country { get; set; }
    public ICollection<PokemonOwner> PokemonOwners { get; set; }
}
