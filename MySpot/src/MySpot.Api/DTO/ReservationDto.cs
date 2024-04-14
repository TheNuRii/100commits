namespace MySpot.Api.DTO;

public class ReservationDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ParkingSpotId { get; set; }
    public string EmplyeeName { get; set; }
    public string LicensePlate { get; set; }
    public DateTime Data { get; set; }
}

