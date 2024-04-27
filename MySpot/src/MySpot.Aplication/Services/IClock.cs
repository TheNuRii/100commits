using MySpot.Core.ValueObjects;

namespace MySpot.Aplication.Services;

public interface IClock
{
    Date Current();
}