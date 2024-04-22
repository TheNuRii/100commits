using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.DTO;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Services;

public class ReservationService
{
    private static readonly Clock Clock = new();
    private readonly List<WeeklyParkingSpot> _weeklyParkingSpots;
    
    

    public ReservationService(List<WeeklyParkingSpot> weeklyParkingSpots)
    {
        _weeklyParkingSpots = weeklyParkingSpots;
    }
    public ReservationDto Get(Guid id)
        => GetAllWeekly().SingleOrDefault(x => x.Id == id);

    public IEnumerable<ReservationDto> GetAllWeekly()
        => _weeklyParkingSpots.SelectMany(x => x.Reservations)
            .Select(x => new ReservationDto
            {
                Id = x.Id,
                ParkingSpotId = x.ParkingSpotId,
                EmplyeeName = x.EmploteeName,
                Date = x.Date.Value.Date,
            });

    public Guid? Crete(CreateReservation command)
    {
        var weeklyParkingSpot = _weeklyParkingSpots.SingleOrDefault(x => x.Id == command.ParkingSpotId);
        if (weeklyParkingSpot is null)
            return default;

        var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName,
            command.LicencePlate, new Date(command.Date));
        weeklyParkingSpot.Addreservation(reservation, Clock.Current());
        
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

        if (existingReservation.Date <= Clock.Current())
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
        => _weeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(r => r.Id == reservationId));
}

