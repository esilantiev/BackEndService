using System.Threading.Tasks;
using System.Web.Http;
using Ises.Application.Managers;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.IsolationPointsDto;

namespace Ises.BackOffice.Api.Controllers
{
    public class IsolationPointController : ApiController
    {
        private readonly IIsolationPointManager isolationPointManager;

        public IsolationPointController(IIsolationPointManager isolationPointManager)
        {
            this.isolationPointManager = isolationPointManager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetIsolationPoints(IsolationPointFilter filter)
        {
            var isolationPoints = await isolationPointManager.GetIsolationPointsAsync(filter);
            return Ok(isolationPoints);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateIsolationPoint(IsolationPointDto isolationPointDto)
        {
            var isolationPointId = await isolationPointManager.CreateIsolationPointAsync(isolationPointDto);
            return Ok(isolationPointId);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateIsolationPoint(IsolationPointDto isolationPointDto)
        {
            await isolationPointManager.UpdateIsolationPointAsync(isolationPointDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveIsolationPoint(long id)
        {
            await isolationPointManager.RemoveIsolationPointAsync(id);
            return Ok();
        }
    }
}
