using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.DTO;

namespace MySpot.Api.Services;

public class ReservationService
{
    private static readonly List<WeeklyParkingSpot> WeeklyParkingSpots = new()
    {
        new WeeklyParkingSpot(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddDays((7)), "P1"),
        new WeeklyParkingSpot(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddDays((7)), "P2"),
        new WeeklyParkingSpot(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddDays((7)), "P3"),
        new WeeklyParkingSpot(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddDays((7)), "P4"),
        new WeeklyParkingSpot(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddDays((7)), "P5"),
    };

    public Reservation Get(Guid id)
        => GetAllWeekly().SingleOrDefault(x => x.Id == id);

    public IEnumerable<Reservation> GetAllWeekly() 
        => WeeklyParkingSpots.SelectMany(x => x.Reservations);

    public Guid? Crete(CreateReservation command)
    {
        var weeklyParkingSpot = WeeklyParkingSpots.SingleOrDefault(x => x.Id == command.ParkingSpotId);
        if (weeklyParkingSpot is null)
            return default;

        var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName,
            command.LicencePlate, command.Date);
        weeklyParkingSpot.Addreservation(reservation);
        
        return reservation.Id;
    }

    public bool Update(Reservation reservation)
    {
        var existingReservation = Reservations.SingleOrDefault(x => x.Id == reservation.Id);
        if (existingReservation == null)
            return false;

        if (existingReservation.Date <= DateTime.UtcNow)
            return false;
        
        if (string.IsNullOrWhiteSpace(reservation.LicensePlate))
            return default;

        existingReservation.LicensePlate = reservation.LicensePlate;
        return true;
    }

    public bool Delete(int id)
    {
        var existingReservation = Reservations.SingleOrDefault(x => x.Id == id);
        if (existingReservation == null)
            return false;

        Reservations.Remove(existingReservation);
        return true;
    }
}

