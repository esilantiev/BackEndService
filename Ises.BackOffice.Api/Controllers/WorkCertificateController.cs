using System.Threading.Tasks;
using System.Web.Http;
using Ises.Application.Managers;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.WorkCertificatesDto;
using Ises.Contracts.WorkCertificatesDto;

namespace Ises.BackOffice.Api.Controllers
{
    public class WorkCertificateController : ApiController
    {
        private readonly IWorkCertificateManager workCerfificateManager;

        public WorkCertificateController(IWorkCertificateManager workCerfificateManager)
        {
            this.workCerfificateManager = workCerfificateManager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetWorkCertificates(WorkCertificateFilter filter)
        {
            var workCerfificates = await workCerfificateManager.GetWorkCertificatesAsync(filter);
            return Ok(workCerfificates);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateWorkCertificate(WorkCertificateDto workCerfificateDto)
        {
            var workCerfificateId = await workCerfificateManager.CreateWorkCertificateAsync(workCerfificateDto);
            return Ok(workCerfificateId);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateWorkCertificate(WorkCertificateDto workCerfificateDto)
        {
            await workCerfificateManager.UpdateWorkCertificateAsync(workCerfificateDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveWorkCertificate(long id)
        {
            await workCerfificateManager.RemoveWorkCertificateAsync(id);
            return Ok();
        }
    }
}
