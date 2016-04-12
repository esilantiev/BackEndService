using System.Threading.Tasks;
using System.Web.Http;
using Ises.Application.Managers;
using Ises.Contracts.CertificatesUsersDto;
using Ises.Contracts.ClientFilters;

namespace Ises.BackOffice.Api.Controllers
{
    public class CertificateUserController : ApiController
    {
        private readonly ICertificateUserManager certificateUserManager;

        public CertificateUserController(ICertificateUserManager certificateUserManager)
        {
            this.certificateUserManager = certificateUserManager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetCertificateUsers(CertificateUserFilter filter)
        {
            var certificateUsers = await certificateUserManager.GetCertificateUsersAsync(filter);
            return Ok(certificateUsers);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateCertificateUser(CertificateUserDto certificateUserDto)
        {
            var certificateUserId = await certificateUserManager.CreateCertificateUserAsync(certificateUserDto);
            return Ok(certificateUserId);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateCertificateUser(CertificateUserDto certificateUserDto)
        {
            await certificateUserManager.UpdateCertificateUserAsync(certificateUserDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveCertificateUser(long id)
        {
            await certificateUserManager.RemoveCertificateUserAsync(id);
            return Ok();
        }
    }
}
