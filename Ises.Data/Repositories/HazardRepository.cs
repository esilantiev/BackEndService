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
using Ises.Domain.Hazards;

namespace Ises.Data.Repositories
{
    public interface IHazardRepository : IRepository
    {
        Task<PagedResult<Hazard>> GetHazardsAsync(HazardFilter hazardFilter);
        Task<long> CreateHazardAsync(Hazard hazard, string mappingScheme);
        Task RemoveHazardAsync(long id);
        Task<Hazard> UpdateHazardAsync(Hazard hazard, string mappingScheme);

        IQueryable<Hazard> GetHazardsQuery(Expression<Func<Hazard, bool>> expression = null, params Expression<Func<Hazard, object>>[] includes);
    }

    public class HazardRepository : BaseRepository, IHazardRepository
    {
        readonly IUnitOfWork unitOfWork;
        private IHazardMappingSchemeRegistrator hazardMappingSchemeRegistrator;

        public HazardRepository(IUnitOfWork unitOfWork, IHazardMappingSchemeRegistrator hazardMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.hazardMappingSchemeRegistrator = hazardMappingSchemeRegistrator;
        }

        public async Task<PagedResult<Hazard>> GetHazardsAsync(HazardFilter filter)
        {
            filter = filter ?? new HazardFilter();

            var result = unitOfWork.Query(GetHazardExpression(filter), filter.PropertiesToInclude);

            List<Hazard> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<Hazard>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetHazardExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreateHazardAsync(Hazard hazard, string mappingScheme)
        {
            hazardMappingSchemeRegistrator.Register();
            var insertedHazard = unitOfWork.Add(hazard, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedHazard.Id;
        }

        public async Task RemoveHazardAsync(long id)
        {
            var hazard = await unitOfWork.Query<Hazard>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(hazard);
            await unitOfWork.SaveAsync();
        }

        public async Task<Hazard> UpdateHazardAsync(Hazard hazard, string mappingScheme)
        {
            hazardMappingSchemeRegistrator.Register();
            var updatedHazard = unitOfWork.Add(hazard, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedHazard;
        }

        public IQueryable<Hazard> GetHazardsQuery(Expression<Func<Hazard, bool>> expression = null, params Expression<Func<Hazard, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<Hazard, bool>> GetHazardExpression(HazardFilter filter)
        {
            Expression<Func<Hazard, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = x => x.Id == filter.Id;
            }
            return expression;
        }
    }
}
