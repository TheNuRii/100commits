namespace MySpot.Api.ValueObjects;

public sealed record ParkingSpotId
{
    public Guid Value { get; }

    public ParkingSpotId(Guid value)
    {
        if (value == Guid.Empty)
            throw new Exception();
        Value = value;
    }

    public static ParkingSpotId Create() => new(Guid.NewGuid());
    
    public static implicit operator Guid(ParkingSpotId parkingSpotId) => parkingSpotId.Value;

    public static implicit operator ParkingSpotId(Guid parkingSpotId) => new(parkingSpotId);
}