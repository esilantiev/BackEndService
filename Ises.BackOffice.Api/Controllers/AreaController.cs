using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;
using Ises.Application.Managers;
using Ises.Contracts.AreasDto;
using Ises.Contracts.ClientFilters;

namespace Ises.BackOffice.Api.Controllers
{
    public class AreaController : ApiController
    {
        private readonly IAreaManager areaManager;
        private readonly IValidatorFactory validatorFactory;

        public AreaController(IAreaManager areaManager, IValidatorFactory validatorFactory)
        {
            this.areaManager = areaManager;
            this.validatorFactory = validatorFactory;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetAreas(AreaFilter filter)
        {
            var apiResult = await areaManager.GetAreasAsync(filter);
            return Ok(apiResult);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateArea(AreaDto areaDto)
        {
            var areaDtoValidator = validatorFactory.GetValidator<AreaDto>();
            await areaDtoValidator.ValidateAndThrowAsync(areaDto);

            var apiResult = await areaManager.CreateAreaAsync(areaDto);
            return Ok(apiResult);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateArea(AreaDto areaDto)
        {
            var apiResult = await areaManager.UpdateAreaAsync(areaDto);
            return Ok(apiResult);
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveArea(long id)
        {
            var apiResult = await areaManager.RemoveAreaAsync(id);
            return Ok(apiResult);
        }
    }
}
