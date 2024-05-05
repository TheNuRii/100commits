using MySpot.Core.Exceptions;

namespace MySpot.Core.ValueObjects;

public sealed record EmployeeName
{
    public string Value { get; }

    public EmployeeName(string value)
    {
        if (value == null)
            throw new InvaildEmployeeNameException(value);
        Value = value;
    }

    public static implicit operator string(EmployeeName name)
        => name.Value;

    public static implicit operator EmployeeName(string value)
        => new(value);
}