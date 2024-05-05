using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Repositories;

public interface IWeeklyParkingSpotRepository
{
    public WeeklyParkingSpot GetSpot(ParkingSpotId id); 
    public IEnumerable<WeeklyParkingSpot> GetAll(object uknown);
    void Add(WeeklyParkingSpot weeklyParkingSpot);
    void Update(WeeklyParkingSpot weeklyParkingSpot);
    void Delete(WeeklyParkingSpot weeklyParkingSpot);
}