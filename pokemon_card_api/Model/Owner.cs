namespace pokemon_card_api.Model;

class Owner
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Gym { get; set; }
    public string? Country { get; set; }
}
