using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.IsolationPointsDto;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.IsolationPoints;

namespace Ises.Application.Managers
{
    public interface IIsolationPointManager : IManager
    {
        Task<PagedResult<IsolationPointDto>> GetIsolationPointsAsync(IsolationPointFilter isolationPointFilter);
        Task RemoveIsolationPointAsync(long id);
        Task<long> CreateIsolationPointAsync(IsolationPointDto isolationPointDto);
        Task<long> UpdateIsolationPointAsync(IsolationPointDto isolationPointDto);
    }

    public class IsolationPointManager : IIsolationPointManager
    {
        readonly IIsolationPointRepository isolationPointRepository;

        public IsolationPointManager(IIsolationPointRepository isolationPointRepository)
        {
            this.isolationPointRepository = isolationPointRepository;
        }

        public async Task<PagedResult<IsolationPointDto>> GetIsolationPointsAsync(IsolationPointFilter isolationPointFilter)
        {
            var isolationPointsPagedResult = await isolationPointRepository.GetIsolationPointsAsync(isolationPointFilter);

            var isolationPointsModelPagedResult = new PagedResult<IsolationPointDto>();
            Mapper.Map(isolationPointsPagedResult, isolationPointsModelPagedResult);
            return isolationPointsModelPagedResult;
        }

        public Task RemoveIsolationPointAsync(long id)
        {
            return isolationPointRepository.RemoveIsolationPointAsync(id);
        }

        public async Task<long> CreateIsolationPointAsync(IsolationPointDto isolationPointDto)
        {
            var isolationPoint = new IsolationPoint();
            Mapper.Map(isolationPointDto, isolationPoint);
            var rowsUpdated = await isolationPointRepository.CreateIsolationPointAsync(isolationPoint, isolationPointDto.MappingScheme);
            return rowsUpdated;
        }

        public async Task<long> UpdateIsolationPointAsync(IsolationPointDto isolationPointDto)
        {
            var isolationPoint = new IsolationPoint();
            Mapper.Map(isolationPointDto, isolationPoint);
            var rowsUpdated = await isolationPointRepository.UpdateIsolationPointAsync(isolationPoint, isolationPointDto.MappingScheme);
            return rowsUpdated;
        }

    }
}