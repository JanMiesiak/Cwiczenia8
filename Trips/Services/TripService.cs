using Microsoft.EntityFrameworkCore;
using Trips.Models;
using Trips.Models.DTOs;
   
namespace Trips.Services;



public class TripService : ITripsServices
    {
        private readonly Apbd2024Context _context;

        public TripService(Apbd2024Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<TripGetAllResponse>> GetAllTripsAsync()
        {
            var trips = await _context.Trips
                .Select(t => new TripGetAllResponse
                {
                    Name = t.Name,
                    Description = t.Description,
                    DateFrom = t.DateFrom,
                    DateTo = t.DateTo,
                    MaxPeople = t.MaxPeople,
                    Countries = t.IdCountries.Select(c => new CountryGetAllRequests { Name = c.Name }).ToList(),
                    Clients = t.ClientTrips.Select(ct => new ClientResponse { FirstName = ct.IdClientNavigation.FirstName, LastName = ct.IdClientNavigation.LastName }).ToList()
                })
                .ToListAsync();

            return trips;
        }

        public async Task<bool> AssignClientToTrip(AddClientToTrip clientToTrip)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == clientToTrip.PESEL);

            if (client == null)
            {
                client = new Client
                {
                    FirstName = clientToTrip.FirstName,
                    LastName = clientToTrip.LastName,
                    Email = clientToTrip.Email,
                    Telephone = clientToTrip.Telephone,
                    Pesel = clientToTrip.PESEL
                };

                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
            }

            var trip = await _context.Trips.FindAsync(clientToTrip.TripID);

            if (trip == null)
            {
                return false;
            }

            var existingClientTrip = await _context.ClientTrips.SingleOrDefaultAsync(ct => ct.IdClient == client.IdClient && ct.IdTrip == trip.IdTrip);

            if (existingClientTrip != null)
            {
                return false;
            }

            var clientTrip = new ClientTrip
            {
                IdClient = client.IdClient,
                IdTrip = trip.IdTrip,
                PaymentDate = clientToTrip.PaymentDate,
                RegisteredAt = DateTime.UtcNow
            };

            _context.ClientTrips.Add(clientTrip);
            await _context.SaveChangesAsync();

            return true;
        }
    }