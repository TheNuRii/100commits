namespace pokemon_card_api.Model;

public class Pokemon
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public ICollection<Rewiew> Rewiews { get; set;}
}

