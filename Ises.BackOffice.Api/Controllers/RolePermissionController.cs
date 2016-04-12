using System.Threading.Tasks;
using System.Web.Http;
using Ises.Application.Managers;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.RolePermissionsDto;

namespace Ises.BackOffice.Api.Controllers
{
    public class RolePermissionController : ApiController
    {
        private readonly IRolePermissionManager rolePermissionManager;

        public RolePermissionController(IRolePermissionManager rolePermissionManager)
        {
            this.rolePermissionManager = rolePermissionManager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetRolePermissions(RolePermissionFilter filter)
        {
            var rolePermissions = await rolePermissionManager.GetRolePermissionsAsync(filter);
            return Ok(rolePermissions);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateRolePermission(RolePermissionDto rolePermissionDto)
        {
            var rolePermissionId = await rolePermissionManager.CreateRolePermissionAsync(rolePermissionDto);
            return Ok(rolePermissionId);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateRolePermission(RolePermissionDto rolePermissionDto)
        {
            await rolePermissionManager.UpdateRolePermissionAsync(rolePermissionDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveRolePermission(long id)
        {
            await rolePermissionManager.RemoveRolePermissionAsync(id);
            return Ok();
        }
    }
}
