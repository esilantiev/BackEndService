using System.Threading.Tasks;
using System.Web.Http;
using Ises.Application.Managers;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.HazardsDto;

namespace Ises.BackOffice.Api.Controllers
{
    public class HazardController : ApiController
    {
        private readonly IHazardManager hazardManager;

        public HazardController(IHazardManager hazardManager)
        {
            this.hazardManager = hazardManager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetHazards(HazardFilter filter)
        {
            var hazards = await hazardManager.GetHazardsAsync(filter);
            return Ok(hazards);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateHazard(HazardDto hazardDto)
        {
            var hazardId = await hazardManager.CreateHazardAsync(hazardDto);
            return Ok(hazardId);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateHazard(HazardDto hazardDto)
        {
            await hazardManager.UpdateHazardAsync(hazardDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveHazard(long id)
        {
            await hazardManager.RemoveHazardAsync(id);
            return Ok();
        }
    }
}
