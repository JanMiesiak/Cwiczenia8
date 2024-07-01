using Trips.Models.DTOs;
using Trips.Services;
using Microsoft.AspNetCore.Mvc;

namespace Trips.Controllers;

[Route("api/trips")]
[ApiController]
public class TripController : ControllerBase
{
    private readonly ITripsServices _tripsService;

    public TripController(ITripsServices tripsServices)
    {
        _tripsService = tripsServices;
    }
        
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var trips = await _tripsService.GetAllTripsAsync();
        return Ok(trips);
    }

    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AddClientToTrip(int idTrip, AddClientToTrip clientToTrip)
    {
        if (idTrip != clientToTrip.TripID)
        {
            return Conflict("No trip found for the provided Id.");
        }

        var addClient = await _tripsService.AssignClientToTrip(clientToTrip);

        if (addClient)
        {
            return Ok();
        }

        return Conflict("Client is already registered on this trip.");
    }
}
        
