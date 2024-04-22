using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Entities;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Controllers;

[Route("reservations")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly Clock _clock;
    private static readonly Clock Clock = new();
    private static readonly ReservationService _service = new(new()
        {
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(Clock.Current()),  "P1"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(Clock.Current()),  "P2"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(Clock.Current()),  "P3"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(Clock.Current()),  "P4"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(Clock.Current()),  "P5"),
        });

    public ReservationsController(Clock clock)
    {
        _clock = clock;
    }
    
    [HttpGet]
    public ActionResult<List<ReservationDto>> Get() => Ok(_service.GetAllWeekly());

    [HttpGet("{id:guid}")]
    public ActionResult<ReservationDto> Get(Guid id)
    {
        var reservation = _service.Get(id);
        if (reservation is null)
            return NotFound();

        return Ok(reservation);
    }
    
    [HttpPost]
    public ActionResult Post(CreateReservation command)
    {
        var id = _service.Crete(command with {ReservationId = Guid.NewGuid()});
        if (id is null)
            return BadRequest();

        return CreatedAtAction(nameof(Get), new {id}, null);
    }
    
    [HttpPut("{id:guid}")]
    public ActionResult Put(Guid id, ChangeReservationLicencePlate command)
    {
        if (_service.Update(command with { ReservationId = id }))
            return NoContent();
            
        return NotFound();
    }
    
    [HttpDelete("{id:guid}")]
    public ActionResult Delete(Guid id)
    { 
        if (_service.Delete(new DeleteReservation(id)))
            return NoContent();
        
        return NotFound();
    }
}