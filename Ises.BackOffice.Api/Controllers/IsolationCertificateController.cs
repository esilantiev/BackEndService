using System.Threading.Tasks;
using System.Web.Http;
using Ises.Application.Managers;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.IsolationCertificatesDto;

namespace Ises.BackOffice.Api.Controllers
{
    public class IsolationCertificateController : ApiController
    {
        private readonly IIsolationCertificateManager isolationCertificateManager;

        public IsolationCertificateController(IIsolationCertificateManager isolationCertificateManager)
        {
            this.isolationCertificateManager = isolationCertificateManager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetIsolationCertificates(IsolationCertificateFilter filter)
        {
            var isolationCertificates = await isolationCertificateManager.GetIsolationCertificatesAsync(filter);
            return Ok(isolationCertificates);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateIsolationCertificate(IsolationCertificateDto isolationCertificateDto)
        {
            var isolationCertificateId = await isolationCertificateManager.CreateIsolationCertificateAsync(isolationCertificateDto);
            return Ok(isolationCertificateId);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateIsolationCertificate(IsolationCertificateDto isolationCertificateDto)
        {
            await isolationCertificateManager.UpdateIsolationCertificateAsync(isolationCertificateDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveIsolationCertificate(long id)
        {
            await isolationCertificateManager.RemoveIsolationCertificateAsync(id);
            return Ok();
        }
    }
}
