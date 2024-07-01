using Trips.Services;
using Microsoft.AspNetCore.Mvc;

namespace Trips.Controllers;

 [Route("api/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientServices _clientServices;

        public ClientController(IClientServices clientServices)
        {
            _clientServices = clientServices;
        }

        [HttpDelete("{idClient}")]
        public async Task<IActionResult> DeleteClient(int idClient)
        {
            bool hasAssignedTours = await _clientServices.HasAssignedTours(idClient);

            if (hasAssignedTours)
            {
                return Conflict("Client has at least one assigned trip and cannot be deleted.");
            }

            bool deleteSuccessful = await _clientServices.DeleteClient(idClient);

            if (deleteSuccessful)
            {
                return Ok();
            }

            return NotFound();
        }
    }
