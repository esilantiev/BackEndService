using System.Threading.Tasks;
using System.Web.Http;
using Ises.Application.Managers;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.HazardControlsDto;

namespace Ises.BackOffice.Api.Controllers
{
    public class HazardControlController : ApiController
    {
        private readonly IHazardControlManager hazardControlManager;

        public HazardControlController(IHazardControlManager hazardControlManager)
        {
            this.hazardControlManager = hazardControlManager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetHazardControls(HazardControlFilter filter)
        {
            var hazardControls = await hazardControlManager.GetHazardControlsAsync(filter);
            return Ok(hazardControls);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateHazardControl(HazardControlDto hazardControlDto)
        {
            var hazardControlId = await hazardControlManager.CreateHazardControlAsync(hazardControlDto);
            return Ok(hazardControlId);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateHazardControl(HazardControlDto hazardControlDto)
        {
            await hazardControlManager.UpdateHazardControlAsync(hazardControlDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveHazardControl(long id)
        {
            await hazardControlManager.RemoveHazardControlAsync(id);
            return Ok();
        }
    }
}
