using MySpot.Api.Entities; 
using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;
using Shouldly;

namespace MySpot.Tests.Unit.Entities;

public class WeeklyParkingSpotTests
{
    [Theory]
    [InlineData("2024-09-09")]
    [InlineData("2024-08-17")]
    public void given_invalid_date_add_reservation_should_fail(string dateString)
    {
        // Arrange
        var now = new DateTime(2022, 08, 10);
        var invalidDate = DateTime.Parse(dateString);
        var weeklyparkiSpot = new WeeklyParkingSpot(Guid.NewGuid(), new Week(now),"P1");
        var reservation = new Reservation(Guid.NewGuid(), weeklyparkiSpot.Id, "John Doe",
            "XYZ123", new Date(invalidDate));
        
        // Act
        var exception = Record.Exception(() => weeklyparkiSpot.Addreservation(reservation, new Date(now)));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidReservationDateException>();
    }

    public  WeeklyParkingSpotTests()
    {
        
    }
}