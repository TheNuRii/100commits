using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers;

[Route("reservations")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly ReservationService _service = new();
    
    [HttpGet]
    public ActionResult<List<Reservation>> Get() => Ok(_service.GetAllWeekly());

    [HttpGet("{id:guid}")]
    public ActionResult<Reservation> Get(Guid id)
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
    public ActionResult Put(Guid id,Reservation reservation)
    {
        reservation.Id = id;
        if (_service.Update(reservation))
            return NoContent();

        return NotFound();
    }
    
    [HttpDelete("{id:guid}")]
    public ActionResult Delete(Guid id)
    {
        var reservation = _service.Get(id);
        if (_service.Update(reservation))
            return NoContent();

        return NotFound();
    }
}