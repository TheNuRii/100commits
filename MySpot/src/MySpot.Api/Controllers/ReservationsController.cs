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
    public void Get()
    {
        
    }

    [HttpPost]
    public void Post(Reservation reservation)
    {
        if (ParkingSpotsNames.All(x => x != reservation.ParkingSpotName))
        {
            HttpContext.Response.StatusCode = 400;
            return;
        }
        
        reservation.Date = DateTime.UtcNow.AddDays(1).Date;
        var reservationAlreadyExists = Reservations.Any(x =>
            x.ParkingSpotName == reservation.ParkingSpotName &&
            x.Date.Date == reservation.Date.Date);

        if (reservationAlreadyExists)
        {
            HttpContext.Response.StatusCode = 400;
            return;
        }
        
        reservation.Id = _id;
        _id++;
        Reservations.Add(reservation);
    }
}