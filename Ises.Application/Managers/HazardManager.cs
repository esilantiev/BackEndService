using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.HazardsDto;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.Hazards;

namespace Ises.Application.Managers
{
    public interface IHazardManager : IManager
    {
        Task<ApiResult> GetHazardsAsync(HazardFilter hazardFilter);
        Task<ApiResult> RemoveHazardAsync(long id);
        Task<ApiResult> CreateHazardAsync(HazardDto hazardDto);
        Task<ApiResult> UpdateHazardAsync(HazardDto hazardDto);
    }

    public class HazardManager : IHazardManager
    {
        readonly IHazardRepository hazardRepository;

        public HazardManager(IHazardRepository hazardRepository)
        {
            this.hazardRepository = hazardRepository;
        }

        public async Task<ApiResult> GetHazardsAsync(HazardFilter hazardFilter)
        {
            var hazardsPagedResult = await hazardRepository.GetHazardsAsync(hazardFilter);

            var hazardsDtoPagedResult = new PagedResult<HazardDto>();
            Mapper.Map(hazardsPagedResult, hazardsDtoPagedResult);
            return new ApiResult(MessageType.Success, hazardsDtoPagedResult);
        }

        public async Task<ApiResult> RemoveHazardAsync(long id)
        {
            await hazardRepository.RemoveHazardAsync(id);
            return new ApiResult(MessageType.Success);
        }

        public async Task<ApiResult> CreateHazardAsync(HazardDto hazardDto)
        {
            var hazard = new Hazard();
            Mapper.Map(hazardDto, hazard);
            var insertedId = await hazardRepository.CreateHazardAsync(hazard, hazardDto.MappingScheme);

            var apiResult = new ApiResult(MessageType.Success);
            apiResult.AdditionalDetails.Add("insertedId", insertedId);

            return apiResult;
        }

        public async Task<ApiResult> UpdateHazardAsync(HazardDto hazardDto)
        {
            var hazard = new Hazard();
            Mapper.Map(hazardDto, hazard);
            var updatedHazard = await hazardRepository.UpdateHazardAsync(hazard, hazardDto.MappingScheme);

            var apiResult = new ApiResult(MessageType.Success);
            return apiResult;
        }

    }
}
