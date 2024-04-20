using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Entities;

public class Reservation
{
    public Guid Id { get;}
    public Guid ParkingSpotId  { get; private set; }
    public string EmploteeName { get; private set; }
    public LicensePlate LicensePlate { get; private set; }
    public Date Date { get; private set; }
    
    public Reservation(Guid id, Guid parkingSpotId, string emploteeName, LicensePlate licensePlate, Date date)
    {
        Id = id;
        ParkingSpotId = parkingSpotId;
        EmploteeName = emploteeName;
        ChangeLicensePlate(licensePlate);
        Date = date;
    }

    public void ChangeLicensePlate(LicensePlate licensePlate)
        => LicensePlate = licensePlate;
}