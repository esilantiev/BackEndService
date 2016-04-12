using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.LocationsDto;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.Locations;

namespace Ises.Application.Managers
{
    public interface ILocationManager : IManager
    {
        Task<ApiResult> GetLocationsAsync(LocationFilter locationFilter);
        Task<ApiResult> RemoveLocationAsync(long id);
        Task<ApiResult> CreateLocationAsync(LocationDto locationDto);
        Task<ApiResult> UpdateLocationAsync(LocationDto locationDto);
    }

    public class LocationManager : ILocationManager
    {
        readonly ILocationRepository locationRepository;

        public LocationManager(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        public async Task<ApiResult> GetLocationsAsync(LocationFilter locationFilter)
        {
            var locationsPagedResult = await locationRepository.GetLocationsAsync(locationFilter);

            var locationsDtoPagedResult = new PagedResult<LocationDto>();
            Mapper.Map(locationsPagedResult, locationsDtoPagedResult);
            return new ApiResult(MessageType.Success, locationsDtoPagedResult);
        }

        public async Task<ApiResult> RemoveLocationAsync(long id)
        {
            await locationRepository.RemoveLocationAsync(id);
            return new ApiResult(MessageType.Success);
        }

        public async Task<ApiResult> CreateLocationAsync(LocationDto locationDto)
        {
            var location = new Location();
            Mapper.Map(locationDto, location);
            var insertedId = await locationRepository.CreateLocationAsync(location, locationDto.MappingScheme);

            var apiResult = new ApiResult(MessageType.Success);
            apiResult.AdditionalDetails.Add("insertedId", insertedId);

            return apiResult;
        }

        public async Task<ApiResult> UpdateLocationAsync(LocationDto locationDto)
        {
            var location = new Location();
            Mapper.Map(locationDto, location);
            var updatedLocation = await locationRepository.UpdateLocationAsync(location, locationDto.MappingScheme);

            var apiResult = new ApiResult(MessageType.Success);
            return apiResult;
        }

    }
}
