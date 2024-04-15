namespace MySpot.Api.DTO;

public class ReservationDto
{
    public Guid Id { get; set; }
    public Guid ParkingSpotId { get; set; }
    public string EmplyeeName { get; set; }
    public string LicensePlate { get; set; }
    public DateTime Date { get; set; }
}

