using System.Threading.Tasks;
using System.Web.Http;
using Ises.Application.Managers;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.PositionsDto;

namespace Ises.BackOffice.Api.Controllers
{
    public class PositionController : ApiController
    {
        private readonly IPositionManager positionManager;

        public PositionController(IPositionManager positionManager)
        {
            this.positionManager = positionManager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetPositions(PositionFilter filter)
        {
            var positions = await positionManager.GetPositionsAsync(filter);
            return Ok(positions);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreatePosition(PositionDto positionDto)
        {
            var positionId = await positionManager.CreatePositionAsync(positionDto);
            return Ok(positionId);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdatePosition(PositionDto positionDto)
        {
            await positionManager.UpdatePositionAsync(positionDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemovePosition(long id)
        {
            await positionManager.RemovePositionAsync(id);
            return Ok();
        }
    }
}
