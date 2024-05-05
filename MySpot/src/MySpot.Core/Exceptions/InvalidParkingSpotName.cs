namespace MySpot.Core.Exceptions;

public class InvalidParkingSpotName : CustomException
{
    public string ParkingSpotName { get; }

    public InvalidParkingSpotName(string parkingSpotName) 
        : base($"Parking Spot name: {parkingSpotName} is invalid")
    {
        ParkingSpotName = parkingSpotName;
    }
}