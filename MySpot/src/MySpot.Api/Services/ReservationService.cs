using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.DTO;

namespace MySpot.Api.Services;

public class ReservationService
{
    private static readonly List<WeeklyParkingSpot> WeeklyParkingSpots = new()
    {
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-00000001"), DateTime.UtcNow, DateTime.UtcNow.AddDays((7)), "P1"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-00000002"), DateTime.UtcNow, DateTime.UtcNow.AddDays((7)), "P2"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-00000003"), DateTime.UtcNow, DateTime.UtcNow.AddDays((7)), "P3"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-00000004"), DateTime.UtcNow, DateTime.UtcNow.AddDays((7)), "P4"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-00000005"), DateTime.UtcNow, DateTime.UtcNow.AddDays((7)), "P5"),
    };

    public ReservationDto Get(Guid id)
        => GetAllWeekly().SingleOrDefault(x => x.Id == id);

    public IEnumerable<ReservationDto> GetAllWeekly()
        => WeeklyParkingSpots.SelectMany(x => x.Reservations)
            .Select(x => new ReservationDto
            {
                Id = x.Id,
                ParkingSpotId = x.ParkingSpotId,
                EmplyeeName = x.EmploteeName,
                Date = x.Date
            });

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

    public bool Update(ChangeReservationLicencePlate comand)
    {
        var weeklyParkingSpot = Reservations.SingleOrDefault(x => x.Id == reservation.Id);
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

    private WeeklyParkingSpot getWeeklyParkingSpotByReservation(Guid reservationId)
        => WeeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(r => r.Id == reservationId));
}

