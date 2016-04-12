using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.PositionsDto;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.Positions;

namespace Ises.Application.Managers
{
    public interface IPositionManager : IManager
    {
        Task<PagedResult<PositionDto>> GetPositionsAsync(PositionFilter positionFilter);
        Task RemovePositionAsync(long id);
        Task<long> CreatePositionAsync(PositionDto positionDto);
        Task<long> UpdatePositionAsync(PositionDto positionDto);
    }

    public class PositionManager : IPositionManager
    {
        readonly IPositionRepository positionRepository;

        public PositionManager(IPositionRepository positionRepository)
        {
            this.positionRepository = positionRepository;
        }

        public async Task<PagedResult<PositionDto>> GetPositionsAsync(PositionFilter positionFilter)
        {
            var positionsPagedResult = await positionRepository.GetPositionsAsync(positionFilter);

            var positionsDtoPagedResult = new PagedResult<PositionDto>();
            Mapper.Map(positionsPagedResult, positionsDtoPagedResult);
            return positionsDtoPagedResult;
        }

        public Task RemovePositionAsync(long id)
        {
            return positionRepository.RemovePositionAsync(id);
        }

        public async Task<long> CreatePositionAsync(PositionDto positionDto)
        {
            var position = new Position();
            Mapper.Map(positionDto, position);
            var rowsUpdated = await positionRepository.CreatePositionAsync(position, positionDto.MappingScheme);
            return rowsUpdated;
        }

        public async Task<long> UpdatePositionAsync(PositionDto positionDto)
        {
            var position = new Position();
            Mapper.Map(positionDto, position);
            var rowsUpdated = await positionRepository.UpdatePositionAsync(position, positionDto.MappingScheme);
            return rowsUpdated;
        }

    }
}
