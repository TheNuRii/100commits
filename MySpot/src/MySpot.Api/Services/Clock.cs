using MySpot.Core.ValueObjects;

namespace MySpot.Api.Services;

public class Clock : IClock
{
    public Date Current() => new Date(DateTime.UtcNow);
}