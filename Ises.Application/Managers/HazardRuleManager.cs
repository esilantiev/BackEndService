using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.HazardRulesDto;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.HazardRules;

namespace Ises.Application.Managers
{
    public interface IHazardRuleManager : IManager
    {
        Task<ApiResult> GetHazardRulesAsync(HazardRuleFilter hazardRuleFilter);
        Task<ApiResult> RemoveHazardRuleAsync(long id);
        Task<ApiResult> CreateHazardRuleAsync(HazardRuleDto hazardRuleDto);
        Task<ApiResult> UpdateHazardRuleAsync(HazardRuleDto hazardRuleDto);
    }

    public class HazardRuleManager : IHazardRuleManager
    {
        readonly IHazardRuleRepository hazardRuleRepository;

        public HazardRuleManager(IHazardRuleRepository hazardRuleRepository)
        {
            this.hazardRuleRepository = hazardRuleRepository;
        }

        public async Task<ApiResult> GetHazardRulesAsync(HazardRuleFilter hazardRuleFilter)
        {
            var hazardRulesPagedResult = await hazardRuleRepository.GetHazardRulesAsync(hazardRuleFilter);

            var hazardRulesDtoPagedResult = new PagedResult<HazardRuleDto>();
            Mapper.Map(hazardRulesPagedResult, hazardRulesDtoPagedResult);
            return new ApiResult(MessageType.Success, hazardRulesDtoPagedResult);
        }

        public async Task<ApiResult> RemoveHazardRuleAsync(long id)
        {
            await hazardRuleRepository.RemoveHazardRuleAsync(id);
            return new ApiResult(MessageType.Success);
        }

        public async Task<ApiResult> CreateHazardRuleAsync(HazardRuleDto hazardRuleDto)
        {
            var hazardRule = new HazardRule();
            Mapper.Map(hazardRuleDto, hazardRule);
            var insertedId = await hazardRuleRepository.CreateHazardRuleAsync(hazardRule, hazardRuleDto.MappingScheme);
            
            var apiResult = new ApiResult(MessageType.Success);
            apiResult.AdditionalDetails.Add("insertedId", insertedId);

            return apiResult;
        }

        public async Task<ApiResult> UpdateHazardRuleAsync(HazardRuleDto hazardRuleDto)
        {
            var hazardRule = new HazardRule();
            Mapper.Map(hazardRuleDto, hazardRule);
            var updatedHazardRule = await hazardRuleRepository.UpdateHazardRuleAsync(hazardRule, hazardRuleDto.MappingScheme);
            
            var apiResult = new ApiResult(MessageType.Success);
            return apiResult;
        }

    }
}
