using System.Threading.Tasks;
using System.Web.Http;
using Ises.Application.Managers;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.InstallationsDto;

namespace Ises.BackOffice.Api.Controllers
{
    public class InstallationController : ApiController
    {
        private readonly IInstallationManager installationManager;

        public InstallationController(IInstallationManager installationManager)
        {
            this.installationManager = installationManager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetInstallations(InstallationFilter filter)
        {
            var users = await installationManager.GetInstallationsAsync(filter);
            return Ok(users);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateInstallation(InstallationDto installationDto)
        {
            var installationId = await installationManager.CreateInstallationAsync(installationDto);
            return Ok(installationId);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateInstallation(InstallationDto installationDto)
        {
            await installationManager.UpdateInstallationAsync(installationDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveInstallation(long id)
        {
            await installationManager.RemoveInstallationAsync(id);
            return Ok();
        }
    }
}
