using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;

public class WeeklyParkingSpot
{
    private readonly HashSet<Reservation> _reservations = new();
    
    public ParkingSpotId Id { get; } 
    public Week Week { get; private set; }
    public string Name { get; }
    public IEnumerable<Reservation> Reservations => _reservations;
    
    public WeeklyParkingSpot(ParkingSpotId id, Week week, string name)
    {
        Id = id;
        Week = week;
        Name = name;
    }

    public static WeeklyParkingSpot Create(ParkingSpotId id, Week week, ParkingSpotName name)
        => new(id, week, name);
    
    public void AddReservation(Reservation reservation, Date now)
    {
        var isInvalidDate = reservation.Date < Week.From || 
                            reservation.Date > Week.To ||
                            reservation.Date < now;
        if (isInvalidDate)
            throw new InvalidReservationDateException(reservation.Date.Value.Date);

        if (_reservations.Any(x => x.Date == reservation.Date))
            throw new ParkingSpotAlreadyReservedException(Name, reservation.Date.Value.Date);

        _reservations.Add(reservation);
    }
    
    public void RemoveReservation(ReservationId reservationId)
        => _reservations.RemoveWhere(x => x.Id.Equals(reservationId));
    public void RemoveReservations(IEnumerable<Reservation> reservations)
        => _reservations.RemoveWhere(x => reservations.Any(r => r.Id == x.Id));
}

