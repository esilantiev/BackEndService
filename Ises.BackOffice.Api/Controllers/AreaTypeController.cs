using System.Threading.Tasks;
using System.Web.Http;
using Ises.Application.Managers;
using Ises.Contracts.AreaTypesDto;
using Ises.Contracts.ClientFilters;

namespace Ises.BackOffice.Api.Controllers
{
    public class AreaTypeController : ApiController
    {
        private readonly IAreaTypeManager areaTypeManager;

        public AreaTypeController(IAreaTypeManager areaTypeManager)
        {
            this.areaTypeManager = areaTypeManager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetAreaTypes(AreaTypeFilter filter)
        {
            var areaTypes = await areaTypeManager.GetAreaTypesAsync(filter);
            return Ok(areaTypes);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateAreaType(AreaTypeDto areaTypeDto)
        {
            var areaTypeId = await areaTypeManager.CreateAreaTypeAsync(areaTypeDto);
            return Ok(areaTypeId);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateAreaType(AreaTypeDto areaTypeDto)
        {
            await areaTypeManager.UpdateAreaTypeAsync(areaTypeDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveAreaType(long id)
        {
            await areaTypeManager.RemoveAreaTypeAsync(id);
            return Ok();
        }
    }
}
