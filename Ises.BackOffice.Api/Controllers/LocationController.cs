using System.Threading.Tasks;
using System.Web.Http;
using Ises.Application.Managers;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.LocationsDto;

namespace Ises.BackOffice.Api.Controllers
{
    public class LocationController : ApiController
    {
        private readonly ILocationManager locationManager;

        public LocationController(ILocationManager locationManager)
        {
            this.locationManager = locationManager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetLocations(LocationFilter filter)
        {
            var locations = await locationManager.GetLocationsAsync(filter);
            return Ok(locations);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateLocation(LocationDto locationDto)
        {
            var locationId = await locationManager.CreateLocationAsync(locationDto);
            return Ok(locationId);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateLocation(LocationDto locationDto)
        {
            await locationManager.UpdateLocationAsync(locationDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveLocation(long id)
        {
            await locationManager.RemoveLocationAsync(id);
            return Ok();
        }
    }
}
