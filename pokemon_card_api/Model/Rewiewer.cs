namespace pokemon_card_api.Model;

public class Reviewer
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public ICollection<Review>? Reviews { get; set;}
}

