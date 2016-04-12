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
using Ises.Domain.LeadDisciplines;

namespace Ises.Data.Repositories
{
    public interface ILeadDisciplineRepository : IRepository
    {
        Task<PagedResult<LeadDiscipline>> GetLeadDisciplinesAsync(LeadDisciplineFilter leadDisciplineFilter);
        Task<long> CreateLeadDisciplineAsync(LeadDiscipline leadDiscipline, string mappingScheme);
        Task RemoveLeadDisciplineAsync(long id);
        Task<long> UpdateLeadDisciplineAsync(LeadDiscipline leadDiscipline, string mappingScheme);

        IQueryable<LeadDiscipline> GetLeadDisciplinesQuery(Expression<Func<LeadDiscipline, bool>> expression = null, params Expression<Func<LeadDiscipline, object>>[] includes);
    }

    public class LeadDisciplineRepository : BaseRepository, ILeadDisciplineRepository
    {
        readonly IUnitOfWork unitOfWork;
        private ILeadDisciplineMappingSchemeRegistrator leadDisciplineMappingSchemeRegistrator;

        public LeadDisciplineRepository(IUnitOfWork unitOfWork, ILeadDisciplineMappingSchemeRegistrator leadDisciplineMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.leadDisciplineMappingSchemeRegistrator = leadDisciplineMappingSchemeRegistrator;
        }

        public async Task<PagedResult<LeadDiscipline>> GetLeadDisciplinesAsync(LeadDisciplineFilter filter)
        {
            filter = filter ?? new LeadDisciplineFilter();

            var result = unitOfWork.Query(GetLeadDisciplineExpression(filter), filter.PropertiesToInclude);

            List<LeadDiscipline> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<LeadDiscipline>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetLeadDisciplineExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreateLeadDisciplineAsync(LeadDiscipline leadDiscipline, string mappingScheme)
        {
            leadDisciplineMappingSchemeRegistrator.Register();
            var insertedLeadDiscipline = unitOfWork.Add(leadDiscipline, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedLeadDiscipline.Id;
        }

        public async Task RemoveLeadDisciplineAsync(long id)
        {
            var leadDiscipline = await unitOfWork.Query<LeadDiscipline>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(leadDiscipline);
            await unitOfWork.SaveAsync();
        }

        public async Task<long> UpdateLeadDisciplineAsync(LeadDiscipline leadDiscipline, string mappingScheme)
        {
            leadDisciplineMappingSchemeRegistrator.Register();
            var updatedLeadDiscipline = unitOfWork.Add(leadDiscipline, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedLeadDiscipline.Id;
        }

        public IQueryable<LeadDiscipline> GetLeadDisciplinesQuery(Expression<Func<LeadDiscipline, bool>> expression = null, params Expression<Func<LeadDiscipline, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<LeadDiscipline, bool>> GetLeadDisciplineExpression(LeadDisciplineFilter filter)
        {
            Expression<Func<LeadDiscipline, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = x => x.Id == filter.Id;
            }
            return expression;
        }
    }
}
