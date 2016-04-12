using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.AreasDto;
using Ises.Contracts.ClientFilters;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.Areas;

namespace Ises.Application.Managers
{
    public interface IAreaManager : IManager
    {
        Task<ApiResult> GetAreasAsync(AreaFilter areaFilter);
        Task<ApiResult> RemoveAreaAsync(long id);
        Task<ApiResult> CreateAreaAsync(AreaDto areaDto);
        Task<ApiResult> UpdateAreaAsync(AreaDto areaDto);
    }

    public class AreaManager : IAreaManager
    {
        readonly IAreaRepository areaRepository;

        public AreaManager(IAreaRepository areaRepository)
        {
            this.areaRepository = areaRepository;
        }

        public async Task<ApiResult> GetAreasAsync(AreaFilter areaFilter)
        {
            var areasPagedResult = await areaRepository.GetAreasAsync(areaFilter);

            var areasModelPagedResult = new PagedResult<AreaDto>();
            Mapper.Map(areasPagedResult, areasModelPagedResult);
            return new ApiResult(MessageType.Success, areasModelPagedResult);
        }

        public async Task<ApiResult> RemoveAreaAsync(long id)
        {
            await areaRepository.RemoveAreaAsync(id);
            return new ApiResult(MessageType.Success);
        }

        public async Task<ApiResult> CreateAreaAsync(AreaDto areaDto)
        {
            var area = new Area();
            Mapper.Map(areaDto, area);
            var insertedId = await areaRepository.CreateAreaAsync(area, areaDto.MappingScheme);

            var apiResult =  new ApiResult(MessageType.Success);
            apiResult.AdditionalDetails.Add("insertedId", insertedId);

            return apiResult;
        }

        public async Task<ApiResult> UpdateAreaAsync(AreaDto areaDto)
        {
            var area = new Area();
            Mapper.Map(areaDto, area);
            var updatedArea = await areaRepository.UpdateAreaAsync(area, areaDto.MappingScheme);

            var apiResult = new ApiResult(MessageType.Success);
            apiResult.AdditionalDetails.Add("rowVersion", updatedArea.RowVersion);

            return apiResult;
        }

    }
}