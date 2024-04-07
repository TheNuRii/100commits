using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers;

[Route("reservations")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly ReservationService _service = new();
    
    [HttpGet]
    public ActionResult<List<Reservation>> Get() => Ok(_service.GetAll());

    [HttpGet("{id:int}")]
    public ActionResult<Reservation> Get(int id)
    {
        var reservation = _service.Get(id);
        if (reservation is null)
            return NotFound();

        return Ok(reservation);
    }
    
    [HttpPost]
    public ActionResult Post(Reservation reservation)
    {
        var id = _service.Crete(reservation);
        if (id is null)
            return BadRequest();

        return CreatedAtAction(nameof(Get), new {id}, null);
    }
    
    [HttpPut("{id:int}")]
    public ActionResult Put(int id,Reservation reservation)
    {
        reservation.Id = id;
        if (_service.Update(reservation))
            return NoContent();

        return NotFound();
    }
    
    [HttpDelete("/{id:int}")]
    public ActionResult Delete(int id)
    {
        var reservation = _service.Get(id);
        if (_service.Update(reservation))
            return NoContent();

        return NotFound();
    }
}