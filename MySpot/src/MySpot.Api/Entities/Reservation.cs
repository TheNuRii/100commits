using MySpot.Api.Exceptions;

namespace MySpot.Api.Entities;

public class Reservation
{
    public Guid Id { get;}
    public Guid ParkingSpotId  { get; private set; }
    public string EmploteeName { get; private set; }
    public string LicensePlate { get; private set; }
    public DateTime Date { get; private set; }
    
    public Reservation(Guid id, Guid parkingSpotId, string emploteeName, string licensePlate, DateTime date)
    {
        Id = id;
        EmploteeName = emploteeName;
        LicensePlate = licensePlate;
        Date = date;
        ParkingSpotId = parkingSpotId;
    }

    public void ChangeLicensePlate(string licensePlate)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
            throw new EmptyLicensePlateException();

        LicensePlate = licensePlate;
    }
}