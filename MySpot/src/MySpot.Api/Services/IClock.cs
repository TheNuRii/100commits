using MySpot.Api.ValueObjects;

namespace MySpot.Api.Services;

public interface IClock
{
    Date Current();
}