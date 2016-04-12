using System.Threading.Tasks;
using System.Web.Http;
using Ises.Application.Managers;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.OrganizationsDto;

namespace Ises.BackOffice.Api.Controllers
{
    public class OrganizationController : ApiController
    {
        private readonly IOrganizationManager organizationManager;

        public OrganizationController(IOrganizationManager organizationManager)
        {
            this.organizationManager = organizationManager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetOrganizations(OrganizationFilter filter)
        {
            var users = await organizationManager.GetOrganizationsAsync(filter);
            return Ok(users);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateOrganization(OrganizationDto organizationDto)
        {
            var installationId = await organizationManager.CreateOrganizationAsync(organizationDto);
            return Ok(installationId);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateOrganization(OrganizationDto organizationDto)
        {
            await organizationManager.UpdateOrganizationAsync(organizationDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveOrganization(long id)
        {
            await organizationManager.RemoveOrganizationAsync(id);
            return Ok();
        }
    }
}
