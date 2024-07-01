using Trips.Models;
using Microsoft.EntityFrameworkCore;

namespace Trips.Services;

public class ClientService : IClientServices
{
    private readonly Apbd2024Context _context;

    public ClientService(Apbd2024Context context)
    {
        _context = context;
    }

    public async Task<bool> HasAssignedTours(int idClient)
    {
        return await _context.ClientTrips.AnyAsync(ct => ct.IdClient == idClient);
    }
    
    public async Task<bool> DeleteClient(int idClient)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.IdClient == idClient);
            
        if (client == null)
            return false;
            
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
            
        return true;
    }
}