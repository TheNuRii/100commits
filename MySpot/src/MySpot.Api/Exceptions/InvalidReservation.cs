namespace MySpot.Api.Exceptions;

public sealed class InvaliReservationDateException : CustomException
{
    public DateTime Date { get;  }
    public InvaliReservationDateException(DateTime date) 
        : base($"Reservation date: {date:d} is invalid.")
    {
        Date = date;
    }
}

