using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Repositories;

public interface IWeeklyParkingSpotRepository
{
    public WeeklyParkingSpot Get(ParkingSpotId id); 
    public IEnumerable<WeeklyParkingSpot> GetAll();
    void Add(WeeklyParkingSpot weeklyParkingSpot);
    void Update(WeeklyParkingSpot weeklyParkingSpot);
    void Delete(WeeklyParkingSpot weeklyParkingSpot);
}