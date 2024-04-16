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

    public bool Update(ChangeReservationLicencePlate command)
    {
        var weeklyParkingSpopt = GetWeeklyParkingSpotByReservation(command.ReservationId);
        if (weeklyParkingSpopt is null)
            return false;
        
        var existingReservation = weeklyParkingSpopt.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);
        if (existingReservation == null)
            return false;

        if (existingReservation.Date <= DateTime.UtcNow)
            return false;
        
        existingReservation.ChangeLicensePlate(command.LicencePlate);
        return true;
    }

    public bool Delete(DeleteReservation command)
    {
        var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
        if (weeklyParkingSpot is null)
            return false;
        
        var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);
        if (existingReservation == null)
            return false;

        weeklyParkingSpot.RemoveReservation(command.ReservationId);
        return true;
    }

    private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(Guid reservationId)
        => WeeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(r => r.Id == reservationId));
}

