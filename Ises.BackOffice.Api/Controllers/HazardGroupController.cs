using System.Threading.Tasks;
using System.Web.Http;
using Ises.Application.Managers;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.HazardGroupsDto;

namespace Ises.BackOffice.Api.Controllers
{
    public class HazardGroupController : ApiController
    {
        private readonly IHazardGroupManager hazardGroupManager;

        public HazardGroupController(IHazardGroupManager hazardGroupManager)
        {
            this.hazardGroupManager = hazardGroupManager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetHazardGroups(HazardGroupFilter filter)
        {
            var hazardGroups = await hazardGroupManager.GetHazardGroupsAsync(filter);
            return Ok(hazardGroups);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateHazardGroup(HazardGroupDto hazardGroupDto)
        {
            var hazardGroupId = await hazardGroupManager.CreateHazardGroupAsync(hazardGroupDto);
            return Ok(hazardGroupId);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateHazardGroup(HazardGroupDto hazardGroupDto)
        {
            await hazardGroupManager.UpdateHazardGroupAsync(hazardGroupDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveHazardGroup(long id)
        {
            await hazardGroupManager.RemoveHazardGroupAsync(id);
            return Ok();
        }
    }
}
