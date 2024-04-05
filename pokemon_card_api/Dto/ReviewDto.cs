namespace pokemon_card_api.Dto;

public class ReviewDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Text { get; set; }
    public int Rating { get; set; }
}

