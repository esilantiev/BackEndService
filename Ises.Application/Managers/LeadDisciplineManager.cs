using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.LeadDisciplinesDto;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.LeadDisciplines;

namespace Ises.Application.Managers
{
    public interface ILeadDisciplineManager : IManager
    {
        Task<PagedResult<LeadDisciplineDto>> GetLeadDisciplinesAsync(LeadDisciplineFilter leadDisciplineFilter);
        Task RemoveLeadDisciplineAsync(long id);
        Task<long> CreateLeadDisciplineAsync(LeadDisciplineDto leadDisciplineDto);
        Task<long> UpdateLeadDisciplineAsync(LeadDisciplineDto leadDisciplineDto);
    }

    public class LeadDisciplineManager : ILeadDisciplineManager
    {
        readonly ILeadDisciplineRepository leadDisciplineRepository;

        public LeadDisciplineManager(ILeadDisciplineRepository leadDisciplineRepository)
        {
            this.leadDisciplineRepository = leadDisciplineRepository;
        }

        public async Task<PagedResult<LeadDisciplineDto>> GetLeadDisciplinesAsync(LeadDisciplineFilter leadDisciplineFilter)
        {
            var leadDisciplinesPagedResult = await leadDisciplineRepository.GetLeadDisciplinesAsync(leadDisciplineFilter);

            var leadDisciplinesDtoPagedResult = new PagedResult<LeadDisciplineDto>();
            Mapper.Map(leadDisciplinesPagedResult, leadDisciplinesDtoPagedResult);
            return leadDisciplinesDtoPagedResult;
        }

        public Task RemoveLeadDisciplineAsync(long id)
        {
            return leadDisciplineRepository.RemoveLeadDisciplineAsync(id);
        }

        public async Task<long> CreateLeadDisciplineAsync(LeadDisciplineDto leadDisciplineDto)
        {
            var leadDiscipline = new LeadDiscipline();
            Mapper.Map(leadDisciplineDto, leadDiscipline);
            var rowsUpdated = await leadDisciplineRepository.CreateLeadDisciplineAsync(leadDiscipline, leadDisciplineDto.MappingScheme);
            return rowsUpdated;
        }

        public async Task<long> UpdateLeadDisciplineAsync(LeadDisciplineDto leadDisciplineDto)
        {
            var leadDiscipline = new LeadDiscipline();
            Mapper.Map(leadDisciplineDto, leadDiscipline);
            var rowsUpdated = await leadDisciplineRepository.UpdateLeadDisciplineAsync(leadDiscipline, leadDisciplineDto.MappingScheme);
            return rowsUpdated;
        }

    }
}
