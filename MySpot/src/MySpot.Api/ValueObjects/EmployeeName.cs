using System.Data;
using MySpot.Api.Exceptions;

namespace MySpot.Api.ValueObjects;

public sealed record EmployeeName
{
    public string Value { get; }

    public EmployeeName(string value)
    {
        if (value == null)
            throw new Exception();
        Value = value;
    }

    public static implicit operator string(EmployeeName name)
        => name.Value;

    public static implicit operator EmployeeName(string value)
        => new(value);
}