using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.AreaTypesDto;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.AreaTypes;

namespace Ises.Application.Managers
{
    public interface IAreaTypeManager : IManager
    {
        Task<ApiResult> GetAreaTypesAsync(AreaTypeFilter areaTypeFilter);
        Task<ApiResult> RemoveAreaTypeAsync(long id);
        Task<ApiResult> CreateAreaTypeAsync(AreaTypeDto areaTypeDto);
        Task<ApiResult> UpdateAreaTypeAsync(AreaTypeDto areaTypeDto);
    }

    public class AreaTypeManager : IAreaTypeManager
    {
        readonly IAreaTypeRepository areaTypeRepository;

        public AreaTypeManager(IAreaTypeRepository areaTypeRepository)
        {
            this.areaTypeRepository = areaTypeRepository;
        }

        public async Task<ApiResult> GetAreaTypesAsync(AreaTypeFilter areaTypeFilter)
        {
            var areaTypesPagedResult = await areaTypeRepository.GetAreaTypesAsync(areaTypeFilter);

            var areaTypesModelPagedResult = new PagedResult<AreaTypeDto>();
            Mapper.Map(areaTypesPagedResult, areaTypesModelPagedResult);
            return new ApiResult(MessageType.Success, areaTypesModelPagedResult);
        }

        public async Task<ApiResult> RemoveAreaTypeAsync(long id)
        {
            await areaTypeRepository.RemoveAreaTypeAsync(id);
            return new ApiResult(MessageType.Success);
        }

        public async Task<ApiResult> CreateAreaTypeAsync(AreaTypeDto areaTypeDto)
        {
            var areaType = new AreaType();
            Mapper.Map(areaTypeDto, areaType);
            var insertedId = await areaTypeRepository.CreateAreaTypeAsync(areaType, areaTypeDto.MappingScheme);

            var apiResult = new ApiResult(MessageType.Success);
            apiResult.AdditionalDetails.Add("insertedId", insertedId);

            return apiResult;
        }

        public async Task<ApiResult> UpdateAreaTypeAsync(AreaTypeDto areaTypeDto)
        {
            var areaType = new AreaType();
            Mapper.Map(areaTypeDto, areaType);
            var updatedArea = await areaTypeRepository.UpdateAreaTypeAsync(areaType, areaTypeDto.MappingScheme);

            var apiResult = new ApiResult(MessageType.Success);
            return apiResult;
        }

    }
}