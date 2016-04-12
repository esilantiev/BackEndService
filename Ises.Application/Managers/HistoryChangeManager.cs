using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.HistoryChangesDto;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.HistoryChanges;

namespace Ises.Application.Managers
{
    public interface IHistoryChangeManager : IManager
    {
        Task<PagedResult<HistoryChangeDto>> GetHistoryChangesAsync(HistoryChangeFilter historyChangeFilter);
        Task RemoveHistoryChangeAsync(long id);
        Task<long> CreateHistoryChangeAsync(HistoryChangeDto historyChangeDto);
        Task<long> UpdateHistoryChangeAsync(HistoryChangeDto historyChangeDto);
    }

    public class HistoryChangeManager : IHistoryChangeManager
    {
        readonly IHistoryChangeRepository historyChangeRepository;

        public HistoryChangeManager(IHistoryChangeRepository historyChangeRepository)
        {
            this.historyChangeRepository = historyChangeRepository;
        }

        public async Task<PagedResult<HistoryChangeDto>> GetHistoryChangesAsync(HistoryChangeFilter historyChangeFilter)
        {
            var historyChangesPagedResult = await historyChangeRepository.GetHistoryChangesAsync(historyChangeFilter);

            var historyChangesModelPagedResult = new PagedResult<HistoryChangeDto>();
            Mapper.Map(historyChangesPagedResult, historyChangesModelPagedResult);
            return historyChangesModelPagedResult;
        }

        public Task RemoveHistoryChangeAsync(long id)
        {
            return historyChangeRepository.RemoveHistoryChangeAsync(id);
        }

        public async Task<long> CreateHistoryChangeAsync(HistoryChangeDto historyChangeDto)
        {
            var historyChange = new HistoryChange();
            Mapper.Map(historyChangeDto, historyChange);
            var rowsUpdated = await historyChangeRepository.CreateHistoryChangeAsync(historyChange, historyChangeDto.MappingScheme);
            return rowsUpdated;
        }

        public async Task<long> UpdateHistoryChangeAsync(HistoryChangeDto historyChangeDto)
        {
            var historyChange = new HistoryChange();
            Mapper.Map(historyChangeDto, historyChange);
            var rowsUpdated = await historyChangeRepository.UpdateHistoryChangeAsync(historyChange, historyChangeDto.MappingScheme);
            return rowsUpdated;
        }

    }
}