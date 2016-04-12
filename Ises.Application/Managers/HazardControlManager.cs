using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.HazardControlsDto;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.HazardControls;

namespace Ises.Application.Managers
{
    public interface IHazardControlManager : IManager
    {
        Task<ApiResult> GetHazardControlsAsync(HazardControlFilter hazardControlFilter);
        Task<ApiResult> RemoveHazardControlAsync(long id);
        Task<ApiResult> CreateHazardControlAsync(HazardControlDto hazardControlDto);
        Task<ApiResult> UpdateHazardControlAsync(HazardControlDto hazardControlDto);
    }

    public class HazardControlManager : IHazardControlManager
    {
        readonly IHazardControlRepository hazardControlRepository;

        public HazardControlManager(IHazardControlRepository hazardControlRepository)
        {
            this.hazardControlRepository = hazardControlRepository;
        }

        public async Task<ApiResult> GetHazardControlsAsync(HazardControlFilter hazardControlFilter)
        {
            var hazardControlsPagedResult = await hazardControlRepository.GetHazardControlsAsync(hazardControlFilter);

            var hazardControlsDtoPagedResult = new PagedResult<HazardControlDto>();
            Mapper.Map(hazardControlsPagedResult, hazardControlsDtoPagedResult);
            return new ApiResult(MessageType.Success, hazardControlsDtoPagedResult);
        }

        public async Task<ApiResult> RemoveHazardControlAsync(long id)
        {
            await hazardControlRepository.RemoveHazardControlAsync(id);
            return new ApiResult(MessageType.Success);
        }

        public async Task<ApiResult> CreateHazardControlAsync(HazardControlDto hazardControlDto)
        {
            var hazardControl = new HazardControl();
            Mapper.Map(hazardControlDto, hazardControl);
            var insertedId = await hazardControlRepository.CreateHazardControlAsync(hazardControl, hazardControlDto.MappingScheme);
            
            var apiResult = new ApiResult(MessageType.Success);
            apiResult.AdditionalDetails.Add("insertedId", insertedId);

            return apiResult;
        }

        public async Task<ApiResult> UpdateHazardControlAsync(HazardControlDto hazardControlDto)
        {
            var hazardControl = new HazardControl();
            Mapper.Map(hazardControlDto, hazardControl);
            var updatedHazardControl = await hazardControlRepository.UpdateHazardControlAsync(hazardControl, hazardControlDto.MappingScheme);
            
            var apiResult = new ApiResult(MessageType.Success);
            return apiResult;
        }

    }
}
