using System.Threading.Tasks;
using System.Web.Http;
using Ises.Application.Managers;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.ExternalDocumentsDto;

namespace Ises.BackOffice.Api.Controllers
{
    public class ExternalDocumentController : ApiController
    {
        private readonly IExternalDocumentManager externalDocumentManager;

        public ExternalDocumentController(IExternalDocumentManager externalDocumentManager)
        {
            this.externalDocumentManager = externalDocumentManager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetExternalDocuments(ExternalDocumentFilter filter)
        {
            var externalDocuments = await externalDocumentManager.GetExternalDocumentsAsync(filter);
            return Ok(externalDocuments);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateExternalDocument(ExternalDocumentDto externalDocumentDto)
        {
            var externalDocumentId = await externalDocumentManager.CreateExternalDocumentAsync(externalDocumentDto);
            return Ok(externalDocumentId);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateExternalDocument(ExternalDocumentDto externalDocumentDto)
        {
            await externalDocumentManager.UpdateExternalDocumentAsync(externalDocumentDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveExternalDocument(long id)
        {
            await externalDocumentManager.RemoveExternalDocumentAsync(id);
            return Ok();
        }
    }
}
