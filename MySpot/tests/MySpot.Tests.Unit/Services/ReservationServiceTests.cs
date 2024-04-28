using MySpot.Aplication.Commands;
using MySpot.Aplication.Services;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.Repositories;
using MySpot.Tests.Unit.Shared;
using Shouldly;

namespace MySpot.Tests.Unit.Services;

public class ReservationServiceTests
{
    [Fact]
    public void given_reservation_for_not_taken_date_create_reservation_should_succeed()
    {
        // Arrange
        var weeklyParkingSpot = _weeklyParkingSpotRepository.GetAll().First();
        var command = new CreateReservation(weeklyParkingSpot.Id,
            Guid.NewGuid(), DateTime.UtcNow.AddMinutes(5), "John Doe","XYZ123");
        
        //Act
        var reservationId = _reservationService.Create(command);

        reservationId.ShouldNotBeNull();
        reservationId.Value.ShouldBe(command.ReservationId);
    }

    #region Arrange

    private readonly InMemoryWeeklyParkingSpotRepository<Clock> _weeklyParkingSpotRepository;
    private readonly IReservationService _reservationService;
    private readonly IClock _clock;

    public ReservationServiceTests(IClock clock)
    {
        _clock = clock;
        _weeklyParkingSpotRepository = new InMemoryWeeklyParkingSpotRepository<Clock>(_clock);
        _reservationService = new ReservationService(_clock, _weeklyParkingSpotRepository);
    }

    #endregion
}