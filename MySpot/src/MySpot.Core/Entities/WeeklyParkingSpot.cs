using MySpot.Core.Exceptions;

namespace MySpot.Core.Entities;

public class WeeklyParkingSpot
{
    private readonly HashSet<Reservation> _reservations = new();
    
    public Guid Id { get; }

    public Week Week { get; set; }
    
    public string Name { get; }
    public IEnumerable<Reservation> Reservations => _reservations;
    
    public WeeklyParkingSpot(ParkingSpotId id, Week week, string name)
    {
        Id = id;
        Week = week;
        Name = name;
    }
    

    public void Addreservation(Reservation reservation, Date now)
    {
        var isInvalidDate = reservation.Date < Week.From || 
                            reservation.Date > Week.To ||
                            reservation.Date < now;
        if (isInvalidDate)
            throw new InvalidReservationDateException(reservation.Date.Value.Date);
        
        var reservationAlreadyExists = Reservations.Any(x =>
            x.Date == reservation.Date);

        if (reservationAlreadyExists)
            throw new ParkingSpotAlreadyReservedException(Name, reservation.Date.Value.Date);

        _reservations.Add(reservation);
    }
    
    // Comeback later 
    public void RemoveReservation(ReservationId reservationId)
        => _reservations.RemoveWhere(x => x.Id.Equals(reservationId));
}

