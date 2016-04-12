using System.Threading.Tasks;
using System.Web.Http;
using Ises.Application.Managers;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.WorkCertificateCategoriesDto;

namespace Ises.BackOffice.Api.Controllers
{
    public class WorkCertificateCategoryController : ApiController
    {
        private readonly IWorkCertificateCategoryManager workCertificateCategoryManager;

        public WorkCertificateCategoryController(IWorkCertificateCategoryManager workCertificateCategoryManager)
        {
            this.workCertificateCategoryManager = workCertificateCategoryManager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetWorkCertificateCategories(WorkCertificateCategoryFilter filter)
        {
            var workCertificateCategories = await workCertificateCategoryManager.GetWorkCertificateCategoriesAsync(filter);
            return Ok(workCertificateCategories);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateWorkCertificateCategory(WorkCertificateCategoryDto workCertificateCategoryDto)
        {
            var workCertificateCategoryId = await workCertificateCategoryManager.CreateWorkCertificateCategoryAsync(workCertificateCategoryDto);
            return Ok(workCertificateCategoryId);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateWorkCertificateCategory(WorkCertificateCategoryDto workCertificateCategoryDto)
        {
            await workCertificateCategoryManager.UpdateWorkCertificateCategoryAsync(workCertificateCategoryDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveWorkCertificateCategory(long id)
        {
            await workCertificateCategoryManager.RemoveWorkCertificateCategoryAsync(id);
            return Ok();
        }
    }
}
