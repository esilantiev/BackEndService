using System.Threading.Tasks;
using System.Web.Http;
using Ises.Application.Managers;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.UsersDto;

namespace Ises.BackOffice.Api.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserManager userManager;

        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetUsers(UserFilter filter)
        {
            var users = await userManager.GetUsersAsync(filter);
            return Ok(users);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateUser(UserDto userDto)
        {
            var userId = await userManager.CreateUserAsync(userDto);
            return Ok(userId);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateUser(UserDto userDto)
        {
            await userManager.UpdateUserAsync(userDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveUser(long id)
        {
            await userManager.RemoveUserAsync(id);
            return Ok();
        }
    }
}
