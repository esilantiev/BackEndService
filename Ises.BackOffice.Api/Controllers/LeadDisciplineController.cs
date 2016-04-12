using System.Threading.Tasks;
using System.Web.Http;
using Ises.Application.Managers;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.LeadDisciplinesDto;

namespace Ises.BackOffice.Api.Controllers
{
    public class LeadDisciplineController : ApiController
    {
        private readonly ILeadDisciplineManager leadDisciplineManager;

        public LeadDisciplineController(ILeadDisciplineManager leadDisciplineManager)
        {
            this.leadDisciplineManager = leadDisciplineManager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetLeadDisciplines(LeadDisciplineFilter filter)
        {
            var leadDisciplines = await leadDisciplineManager.GetLeadDisciplinesAsync(filter);
            return Ok(leadDisciplines);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateLeadDiscipline(LeadDisciplineDto leadDisciplineDto)
        {
            var leadDisciplineId = await leadDisciplineManager.CreateLeadDisciplineAsync(leadDisciplineDto);
            return Ok(leadDisciplineId);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateLeadDiscipline(LeadDisciplineDto leadDisciplineDto)
        {
            await leadDisciplineManager.UpdateLeadDisciplineAsync(leadDisciplineDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveLeadDiscipline(long id)
        {
            await leadDisciplineManager.RemoveLeadDisciplineAsync(id);
            return Ok();
        }
    }
}
