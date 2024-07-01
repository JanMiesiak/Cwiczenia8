namespace Trips.Services;

public interface IClientServices
{
    Task<bool> DeleteClient(int idClient);
    Task<bool> HasAssignedTours(int idClient);
    
}