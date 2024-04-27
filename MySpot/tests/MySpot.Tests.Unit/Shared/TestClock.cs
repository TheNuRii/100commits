using MySpot.Aplication.Services;
using MySpot.Core.ValueObjects;

namespace MySpot.Tests.Unit.Shared;

public class TestClock : IClock
{
    public Date Current() => new Date(new DateTime(2022, 08, 11));
}