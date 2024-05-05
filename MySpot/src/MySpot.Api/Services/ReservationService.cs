using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Api.Services;

public class ReservationService : IReservationService
{
    private static Clock Clock = new();
    private readonly IEnumerable<IWeeklyParkingSpotRepository> _weeklyParkingSpotsRepositories;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
    public ReservationService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository, IEnumerable<IWeeklyParkingSpotRepository> weeklyParkingSpotsRepositories)
    {
        _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
        _weeklyParkingSpotsRepositories = weeklyParkingSpotsRepositories;
        Clock = clock;
    }
    public ReservationDto Get(Guid id)
        => GetAllWeekly().SingleOrDefault(x => x.Id == id);

    public IEnumerable<ReservationDto> GetAllWeekly()
        => _weeklyParkingSpotRepository.GetAll().SelectMany(x => x.Reservations)
            .Select(x => new ReservationDto
            {
                Id = x.Id,
                ParkingSpotId = x.ParkingSpotId,
                EmplyeeName = x.EmploteeName,
                Date = x.Date.Value.Date,
            });
    public Guid? Create(CreateReservation command)
    {
        var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
        var weeklyParkingSpot = _weeklyParkingSpotsRepositories.SingleOrDefault()!.GetSpot(parkingSpotId);
        if (weeklyParkingSpot is null)
            return default;

        var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName,
            command.LicencePlate, new Date(command.Date));
        weeklyParkingSpot.AddReservation(reservation, new Date());
        
        return reservation.Id;
    }

    public bool Update(ChangeReservationLicencePlate command)
    {
        var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
        if (weeklyParkingSpot is null)
            return false;
        
        var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);
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
        => _weeklyParkingSpotsRepositories.SingleOrDefault()!.GetSpot(reservationId);
}

