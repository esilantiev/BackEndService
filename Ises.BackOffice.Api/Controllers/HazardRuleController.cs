using System.Threading.Tasks;
using System.Web.Http;
using Ises.Application.Managers;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.HazardRulesDto;

namespace Ises.BackOffice.Api.Controllers
{
    public class HazardRuleController : ApiController
    {
        private readonly IHazardRuleManager hazardRuleManager;

        public HazardRuleController(IHazardRuleManager hazardRuleManager)
        {
            this.hazardRuleManager = hazardRuleManager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetHazardRules(HazardRuleFilter filter)
        {
            var hazardRules = await hazardRuleManager.GetHazardRulesAsync(filter);
            return Ok(hazardRules);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateHazardRule(HazardRuleDto hazardRuleDto)
        {
            var hazardRuleId = await hazardRuleManager.CreateHazardRuleAsync(hazardRuleDto);
            return Ok(hazardRuleId);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateHazardRule(HazardRuleDto hazardRuleDto)
        {
            await hazardRuleManager.UpdateHazardRuleAsync(hazardRuleDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveHazardRule(long id)
        {
            await hazardRuleManager.RemoveHazardRuleAsync(id);
            return Ok();
        }
    }
}
