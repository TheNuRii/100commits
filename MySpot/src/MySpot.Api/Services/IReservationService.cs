using MySpot.Api.Commands;
using MySpot.Api.DTO;

namespace MySpot.Api.Services;

public interface IReservationService
{
    ReservationDto Get(Guid id);
    IEnumerable<ReservationDto> GetAllWeekly();
    Guid? Create(CreateReservation command);
    bool Update(ChangeReservationLicencePlate command);
    bool Delete(DeleteReservation command);
}