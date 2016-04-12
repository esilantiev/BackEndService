using System.Threading.Tasks;
using System.Web.Http;
using Ises.Application.Managers;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.UserRolesDto;

namespace Ises.BackOffice.Api.Controllers
{
    public class UserRoleController : ApiController
    {
        private readonly IUserRoleManager userRoleManager;

        public UserRoleController(IUserRoleManager userRoleManager)
        {
            this.userRoleManager = userRoleManager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetUserRoles(UserRoleFilter filter)
        {
            var userRoles = await userRoleManager.GetUserRolesAsync(filter);
            return Ok(userRoles);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateUserRole(UserRoleDto userRoleDto)
        {
            var userRoleId = await userRoleManager.CreateUserRoleAsync(userRoleDto);
            return Ok(userRoleId);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateUserRole(UserRoleDto userRoleDto)
        {
            await userRoleManager.UpdateUserRoleAsync(userRoleDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveUserRole(long id)
        {
            await userRoleManager.RemoveUserRoleAsync(id);
            return Ok();
        }
    }
}
