using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Entities;
using MySpot.Api.Repositories;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Services;

public class ReservationService : IReservationService
{
    private static readonly Clock Clock = new();
    private readonly IEnumerable<IWeeklyParkingSpotRepository> _weeklyParkingSpotsRepositories;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpot;
    public ReservationService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository)
    {
        _weeklyParkingSpot = weeklyParkingSpot;
    }
    public ReservationDto Get(Guid id)
        => GetAllWeekly().SingleOrDefault(x => x.Id == id);

    public IEnumerable<ReservationDto> GetAllWeekly()
        => _weeklyParkingSpotsRepositories.GetAll().SelectMany(x => x.Id)
        .Select(x => new ReservationDto
            {
                Id = x.Id,
                ParkingSpotId = x.ParkingSpotId,
                EmplyeeName = x.EmploteeName,
                Date = x.Date.Value.Date,
            });
    public Guid? Create(CreateReservation command)
    {
        var weeklyParkingSpot = _weeklyParkingSpotsRepositories.SingleOrDefault(x => x.Id == command.ParkingSpotId);
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
        => _weeklyParkingSpotsRepositories.SingleOrDefault(x => x.Reservations.Any(r => r.Id == reservationId));
}

