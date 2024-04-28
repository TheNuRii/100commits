<<<<<<< HEAD:MySpot/src/MySpot.Aplication/Services/ReservationService.cs
<<<<<<< HEAD:MySpot/src/MySpot.Aplication/Services/ReservationService.cs
=======
using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.DTO;
<<<<<<< HEAD
>>>>>>> parent of d63e74c (Create Aplication Layer and refactoring codebase):MySpot/src/MySpot.Api/Services/ReservationService.cs
using MySpot.Api.Repositories;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;
=======
using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.DTO;
using MySpot.Api.ValueObjects;
>>>>>>> parent of 8048d5b (Dependency Inversion Principle):MySpot/src/MySpot.Api/Services/ReservationService.cs
=======
using MySpot.Api.ValueObjects;
using MySpot.Api.Repositories;
>>>>>>> parent of f72881c (Creating new project to capsulete ValueObjects, Exceptions, Entities)

namespace MySpot.Api.Services;

public class ReservationService : IReservationService
{
    private static readonly Clock Clock = new();
    private readonly IEnumerable<WeeklyParkingSpot> _weeklyParkingSpots;
    
<<<<<<< HEAD
<<<<<<< HEAD:MySpot/src/MySpot.Aplication/Services/ReservationService.cs
    public ReservationService(IClock clock, InMemoryWeeklyParkingSpotRepository weeklyParkingSpotRepository)
=======
    public ReservationService(IClock clock,IEnumerable<WeeklyParkingSpot> weeklyParkingSpots)
>>>>>>> parent of 8048d5b (Dependency Inversion Principle):MySpot/src/MySpot.Api/Services/ReservationService.cs
=======
    public ReservationService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository)
>>>>>>> parent of 34bab31 (test)
    {
        _weeklyParkingSpots = weeklyParkingSpots;
    }
    public ReservationDto Get(Guid id)
        => GetAllWeekly().SingleOrDefault(x => x.Id == id);

    public IEnumerable<ReservationDto> GetAllWeekly()
<<<<<<< HEAD:MySpot/src/MySpot.Aplication/Services/ReservationService.cs
<<<<<<< HEAD:MySpot/src/MySpot.Aplication/Services/ReservationService.cs
        => _weeklyParkingSpotsRepository.GetAll().Select(x => new ReservationDto());
           
=======
        => _weeklyParkingSpots.SelectMany(x => x.Reservations)
=======
        => _weeklyParkingSpotsRepository.GetAll().SelectMany(x => x.Id)
>>>>>>> parent of d63e74c (Create Aplication Layer and refactoring codebase):MySpot/src/MySpot.Api/Services/ReservationService.cs
            .Select(x => new ReservationDto
            {
                Id = x.Id,
                ParkingSpotId = x.ParkingSpotId,
                EmplyeeName = x.EmploteeName,
                Date = x.Date.Value.Date,
            });
<<<<<<< HEAD:MySpot/src/MySpot.Aplication/Services/ReservationService.cs
>>>>>>> parent of 8048d5b (Dependency Inversion Principle):MySpot/src/MySpot.Api/Services/ReservationService.cs
=======
>>>>>>> parent of d63e74c (Create Aplication Layer and refactoring codebase):MySpot/src/MySpot.Api/Services/ReservationService.cs

    public Guid? Create(CreateReservation command)
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

