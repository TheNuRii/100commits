namespace pokemon_card_api.Model;

public class Rewiewer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public ICollection<Rewiew> Rewiews { get; set; }
}

