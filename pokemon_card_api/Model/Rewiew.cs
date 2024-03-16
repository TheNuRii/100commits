namespace pokemon_card_api.Model;

public class Rewiew
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Text { get; set; }
    public Rewiewer Rewiewer { get; set; }
    public Pokemon Pokemon { get; set; }
}