using MySpot.Api.Models;

namespace MySpot.Api.Services;

public class ReservationService
{
    private static readonly List<Reservation> Reservations = new();
    private static int _id = 1;
    private static readonly List<string> ParkingSpotsNames = new()
    {
        "P1", "P2", "P3", "P4", "P5"
    };

    public Reservation Get(int id)
        => Reservations.SingleOrDefault(x => x.Id == id);

    public List<Reservation> GetAll() 
        => Reservations;

    public int? Crete(Reservation reservation)
    {
        if (ParkingSpotsNames.All(x => x != reservation.ParkingSpotName))
            return default;
        
        reservation.Date = DateTime.UtcNow.AddDays(1).Date;
        var reservationAlreadyExists = Reservations.Any(x =>
            x.ParkingSpotName == reservation.ParkingSpotName &&
            x.Date.Date == reservation.Date.Date);

        if (reservationAlreadyExists)
            return default;
        
        reservation.Id = _id;
        _id++;
        Reservations.Add(reservation);

        return reservation.Id;
    }

    public bool Update(Reservation reservation)
    {
        var existingReservation = Reservations.SingleOrDefault(x => x.Id == reservation.Id);
        if (existingReservation == null)
            return false;

        existingReservation.LicensePlate = reservation.LicensePlate;
        return true;
    }

    public bool Delete(int id)
    {
        var existingReservation = Reservations.SingleOrDefault(x => x.Id == id);
        if (existingReservation == null)
            return false;

        Reservations.Remove(existingReservation);
        return true;
    }
}

