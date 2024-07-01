using Trips.Models.DTOs;

namespace Trips.Services;

public interface ITripsServices
{
    Task<bool> AssignClientToTrip(AddClientToTrip clientToTrip);
    Task<List<TripGetAllResponse>> GetAllTripsAsync();
    
}