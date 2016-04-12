using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.HazardGroupsDto;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.HazardGroups;

namespace Ises.Application.Managers
{
    public interface IHazardGroupManager : IManager
    {
        Task<ApiResult> GetHazardGroupsAsync(HazardGroupFilter hazardGroupFilter);
        Task<ApiResult> RemoveHazardGroupAsync(long id);
        Task<ApiResult> CreateHazardGroupAsync(HazardGroupDto hazardGroupDto);
        Task<ApiResult> UpdateHazardGroupAsync(HazardGroupDto hazardGroupDto);
    }

    public class HazardGroupManager : IHazardGroupManager
    {
        readonly IHazardGroupRepository hazardGroupRepository;

        public HazardGroupManager(IHazardGroupRepository hazardGroupRepository)
        {
            this.hazardGroupRepository = hazardGroupRepository;
        }

        public async Task<ApiResult> GetHazardGroupsAsync(HazardGroupFilter hazardGroupFilter)
        {
            var hazardGroupsPagedResult = await hazardGroupRepository.GetHazardGroupsAsync(hazardGroupFilter);

            var hazardGroupsDtoPagedResult = new PagedResult<HazardGroupDto>();
            Mapper.Map(hazardGroupsPagedResult, hazardGroupsDtoPagedResult);
            return new ApiResult(MessageType.Success, hazardGroupsDtoPagedResult);
        }

        public async Task<ApiResult> RemoveHazardGroupAsync(long id)
        {
            await hazardGroupRepository.RemoveHazardGroupAsync(id);
            return new ApiResult(MessageType.Success);
        }

        public async Task<ApiResult> CreateHazardGroupAsync(HazardGroupDto hazardGroupDto)
        {
            var hazardGroup = new HazardGroup();
            Mapper.Map(hazardGroupDto, hazardGroup);
            var insertedId = await hazardGroupRepository.CreateHazardGroupAsync(hazardGroup, hazardGroupDto.MappingScheme);

            var apiResult = new ApiResult(MessageType.Success);
            apiResult.AdditionalDetails.Add("insertedId", insertedId);

            return apiResult;
        }

        public async Task<ApiResult> UpdateHazardGroupAsync(HazardGroupDto hazardGroupDto)
        {
            var hazardGroup = new HazardGroup();
            Mapper.Map(hazardGroupDto, hazardGroup);
            var updatedHazardGroup = await hazardGroupRepository.UpdateHazardGroupAsync(hazardGroup, hazardGroupDto.MappingScheme);
            
            var apiResult = new ApiResult(MessageType.Success);
            return apiResult;
        }

    }
}
