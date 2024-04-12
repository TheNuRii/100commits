namespace MySpot.Api.Entities;

public class Reservation
{
    public int Id { get;}
    public string EmploteeName { get; private set; }
    public string ParkingSpotName  { get; private set; }
    public string LicensePlate { get; private set; }
    public DateTime Date { get; private set; }
    
    public Reservation(int id, string emploteeName, string licensePlate, DateTime date)
    {
        Id = id;
        EmploteeName = emploteeName;
        LicensePlate = licensePlate;
        Date = date;
    }

    public void ChangeLicensePlate(string licensePlate)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
            throw new ArgumentException("License plate is invalid.");

        LicensePlate = licensePlate;
    }
}