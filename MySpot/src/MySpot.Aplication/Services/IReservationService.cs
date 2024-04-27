using MySpot.Aplication.Commands;
using MySpot.Aplication.DTO;

namespace MySpot.Aplication.Services;

public interface IReservationService
{
    ReservationDto Get(Guid id);
    IEnumerable<ReservationDto> GetAllWeekly();
    Guid? Create(CreateReservation command);
    bool Update(ChangeReservationLicencePlate command);
    bool Delete(DeleteReservation command);
}