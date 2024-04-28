namespace MySpot.Api.Commands;

public record CreateReservation(Guid ParkingSpotId, 
    Guid ReservationId, DateTime Date, string EmployeeName, string LicencePlate);