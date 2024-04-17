using MySpot.Api.Exceptions;

namespace MySpot.Api.ValueObjects;

public record ParkingSpotName(string Value)
{
    public string Value { get; } = Value ?? throw new CustomException();

    public static implicit operator string(ParkingSpotName name)
        => name.Value;

    public static implicit operator ParkingSpotName(string value)
        => new(value);
}