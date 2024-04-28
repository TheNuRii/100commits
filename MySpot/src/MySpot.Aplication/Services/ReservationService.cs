using MySpot.Api.Repositories;
using MySpot.Aplication.Commands;
using MySpot.Aplication.DTO;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Aplication.Services;

public class ReservationService : IReservationService
{
    private readonly IClock _clock;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotsRepository;
    
    public ReservationService(IClock clock, InMemoryWeeklyParkingSpotRepository weeklyParkingSpotRepository)
    {
        _clock = clock;
        _weeklyParkingSpotsRepository = weeklyParkingSpotRepository;
    }
    public ReservationDto Get(Guid id)
        => GetAllWeekly().SingleOrDefault(x => x.Id == id);

    public IEnumerable<ReservationDto> GetAllWeekly()
        => _weeklyParkingSpotsRepository.GetAll().Select(x => new ReservationDto());
           

    public Guid? Create(CreateReservation command)
    {
        var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
        var weeklyParkingSpot = _weeklyParkingSpotsRepository.Get(parkingSpotId);
        if (weeklyParkingSpot is null)
            return default;

        var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName,
            command.LicencePlate, new Date(command.Date));
        weeklyParkingSpot.Addreservation(reservation, new Date(_clock.Current()));
        
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

        if (existingReservation.Date <= _clock.Current())
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
        => _weeklyParkingSpotsRepository.GetAll()
            .SingleOrDefault(x => x.Reservations.Any(r => r.Id == reservationId));
}

