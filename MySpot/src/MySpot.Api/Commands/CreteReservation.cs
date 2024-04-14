namespace MySpot.Api.Commands;

public record CreateReservation(Guid ParkingSpotId, string EmployeeName, 
    Guid ReservationId,string LicencePlate, DateTime Date);