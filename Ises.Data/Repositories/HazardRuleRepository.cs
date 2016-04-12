using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ises.Contracts.ClientFilters;
using Ises.Core.Common;
using Ises.Core.Infrastructure;
using Ises.Data.MappingSchemes;
using Ises.Domain.HazardRules;

namespace Ises.Data.Repositories
{
    public interface IHazardRuleRepository : IRepository
    {
        Task<PagedResult<HazardRule>> GetHazardRulesAsync(HazardRuleFilter hazardRuleFilter);
        Task<long> CreateHazardRuleAsync(HazardRule hazardRule, string mappingScheme);
        Task RemoveHazardRuleAsync(long id);
        Task<HazardRule> UpdateHazardRuleAsync(HazardRule hazardRule, string mappingScheme);

        IQueryable<HazardRule> GetHazardRulesQuery(Expression<Func<HazardRule, bool>> expression = null, params Expression<Func<HazardRule, object>>[] includes);
    }

    public class HazardRuleRepository : BaseRepository, IHazardRuleRepository
    {
        readonly IUnitOfWork unitOfWork;
        private IHazardRuleMappingSchemeRegistrator hazardRuleMappingSchemeRegistrator;

        public HazardRuleRepository(IUnitOfWork unitOfWork, IHazardRuleMappingSchemeRegistrator hazardRuleMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.hazardRuleMappingSchemeRegistrator = hazardRuleMappingSchemeRegistrator;
        }

        public async Task<PagedResult<HazardRule>> GetHazardRulesAsync(HazardRuleFilter filter)
        {
            filter = filter ?? new HazardRuleFilter();

            var result = unitOfWork.Query(GetHazardRuleExpression(filter), filter.PropertiesToInclude);

            List<HazardRule> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<HazardRule>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetHazardRuleExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreateHazardRuleAsync(HazardRule hazardRule, string mappingScheme)
        {
            hazardRuleMappingSchemeRegistrator.Register();
            var insertedHazardRule = unitOfWork.Add(hazardRule, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedHazardRule.Id;
        }

        public async Task RemoveHazardRuleAsync(long id)
        {
            var hazardRule = await unitOfWork.Query<HazardRule>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(hazardRule);
            await unitOfWork.SaveAsync();
        }

        public async Task<HazardRule> UpdateHazardRuleAsync(HazardRule hazardRule, string mappingScheme)
        {
            hazardRuleMappingSchemeRegistrator.Register();
            var updatedHazardRule = unitOfWork.Add(hazardRule, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedHazardRule;
        }

        public IQueryable<HazardRule> GetHazardRulesQuery(Expression<Func<HazardRule, bool>> expression = null, params Expression<Func<HazardRule, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<HazardRule, bool>> GetHazardRuleExpression(HazardRuleFilter filter)
        {
            Expression<Func<HazardRule, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = x => x.Id == filter.Id;
            }
            return expression;
        }
    }
}
