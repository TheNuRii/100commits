namespace pokemon_card_api.Model;

class Country
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Owner> Owners { get; set; }
}

