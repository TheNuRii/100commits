using MySpot.Core.ValueObjects;

namespace MySpot.Aplication.Services;

public class Clock : IClock
{
    public Date Current() => new Date(DateTime.UtcNow);
}