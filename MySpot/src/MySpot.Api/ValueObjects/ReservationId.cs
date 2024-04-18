namespace MySpot.Api.ValueObjects;

public sealed record ReservationId
{
    public Guid Value { get; }

    public ReservationId(Guid value)
    {
        if (value == Guid.Empty)
            throw new Exception();
        Value = value;
    }

    public static ReservationId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(ReservationId reservationId) => reservationId.Value;

    public static implicit operator ReservationId(Guid reservationId) => new(reservationId);
}