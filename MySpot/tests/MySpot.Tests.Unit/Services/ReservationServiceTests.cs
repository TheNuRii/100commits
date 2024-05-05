using MySpot.Api.Commands;
using MySpot.Api.Services;
using MySpot.Core.Repositories;
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

    private readonly IClock _clock;
    private readonly IEnumerable<IWeeklyParkingSpotRepository> _weeklyParkingSpotRepository;
    private readonly IReservationService _reservationService;

    public ReservationServiceTests()
    {
        _clock = new TestClock();
        _weeklyParkingSpotRepository = new InMemoryWeeklyParkingSpotRepository(_clock);
        _reservationService = new ReservationService(_clock, _weeklyParkingSpotRepository);
    }

    #endregion
}