using System.Threading.Tasks;
using System.Web.Http;
using Ises.Application.Managers;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.HistoryChangesDto;

namespace Ises.BackOffice.Api.Controllers
{
    public class HistoryChangeController : ApiController
    {
        private readonly IHistoryChangeManager historyChangeManager;

        public HistoryChangeController(IHistoryChangeManager historyChangeManager)
        {
            this.historyChangeManager = historyChangeManager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetHistoryChanges(HistoryChangeFilter filter)
        {
            var historyChanges = await historyChangeManager.GetHistoryChangesAsync(filter);
            return Ok(historyChanges);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateHistoryChange(HistoryChangeDto historyChangeDto)
        {
            var historyChangeId = await historyChangeManager.CreateHistoryChangeAsync(historyChangeDto);
            return Ok(historyChangeId);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateHistoryChange(HistoryChangeDto historyChangeDto)
        {
            await historyChangeManager.UpdateHistoryChangeAsync(historyChangeDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveHistoryChange(long id)
        {
            await historyChangeManager.RemoveHistoryChangeAsync(id);
            return Ok();
        }
    }
}
