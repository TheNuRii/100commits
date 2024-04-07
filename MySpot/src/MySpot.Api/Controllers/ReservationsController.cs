using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;

namespace MySpot.Api.Controllers;

[Route("reservations")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private static readonly List<Reservation> Reservations = new();
    private static int _id = 1;
    private static readonly List<string> ParkingSpotsNames = new()
    {
        "P1", "P2", "P3", "P4", "P5"
    };
    
    [HttpGet]
    public ActionResult<List<Reservation>> Get() => Ok(Reservations);

    [HttpGet("{id:int}")]
    public ActionResult<Reservation> Get(int id)
    {
        var reservation = Reservations.SingleOrDefault(x => x.Id == id);
        if (reservation == null)
            return BadRequest();
        return Ok(reservation);
    }
    
    [HttpPost]
    public ActionResult Post(Reservation reservation)
    {
        if (ParkingSpotsNames.All(x => x != reservation.ParkingSpotName))
            return BadRequest();
        
        reservation.Date = DateTime.UtcNow.AddDays(1).Date;
        var reservationAlreadyExists = Reservations.Any(x =>
            x.ParkingSpotName == reservation.ParkingSpotName &&
            x.Date.Date == reservation.Date.Date);

        if (reservationAlreadyExists)
            return BadRequest();
        
        reservation.Id = _id;
        _id++;
        Reservations.Add(reservation);

        return CreatedAtAction(nameof(Get), new { id = reservation.Id }, null);
    }
    
    [HttpPut("/{id:int}")]
    public ActionResult Put(int id,Reservation reservation)
    {
        var existingReservation = Reservations.SingleOrDefault(x => x.Id == id);
        if (existingReservation == null)
            return NotFound();

        existingReservation.LicensePlate = reservation.LicensePlate;
        return NoContent();
    }
    
    [HttpDelete("/{id:int}")]
    public ActionResult Delete(int id)
    {
        var existingReservation = Reservations.SingleOrDefault(x => x.Id == id);
        if (existingReservation == null)
            return NotFound();

        Reservations.Remove(existingReservation);
        return NoContent();
    }
}