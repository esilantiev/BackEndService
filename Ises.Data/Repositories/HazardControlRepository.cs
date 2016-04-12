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
using Ises.Domain.HazardControls;

namespace Ises.Data.Repositories
{
    public interface IHazardControlRepository : IRepository
    {
        Task<PagedResult<HazardControl>> GetHazardControlsAsync(HazardControlFilter hazardControlFilter);
        Task<long> CreateHazardControlAsync(HazardControl hazardControl, string mappingScheme);
        Task RemoveHazardControlAsync(long id);
        Task<HazardControl> UpdateHazardControlAsync(HazardControl hazardControl, string mappingScheme);

        IQueryable<HazardControl> GetHazardControlsQuery(Expression<Func<HazardControl, bool>> expression = null, params Expression<Func<HazardControl, object>>[] includes);
    }

    public class HazardControlRepository : BaseRepository, IHazardControlRepository
    {
        readonly IUnitOfWork unitOfWork;
        private IHazardControlMappingSchemeRegistrator hazardControlMappingSchemeRegistrator;

        public HazardControlRepository(IUnitOfWork unitOfWork, IHazardControlMappingSchemeRegistrator hazardControlMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.hazardControlMappingSchemeRegistrator = hazardControlMappingSchemeRegistrator;
        }

        public async Task<PagedResult<HazardControl>> GetHazardControlsAsync(HazardControlFilter filter)
        {
            filter = filter ?? new HazardControlFilter();

            var result = unitOfWork.Query(GetHazardControlExpression(filter), filter.PropertiesToInclude);

            List<HazardControl> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<HazardControl>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetHazardControlExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreateHazardControlAsync(HazardControl hazardControl, string mappingScheme)
        {
            hazardControlMappingSchemeRegistrator.Register();
            var insertedHazardControl = unitOfWork.Add(hazardControl, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedHazardControl.Id;
        }

        public async Task RemoveHazardControlAsync(long id)
        {
            var hazardControl = await unitOfWork.Query<HazardControl>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(hazardControl);
            await unitOfWork.SaveAsync();
        }

        public async Task<HazardControl> UpdateHazardControlAsync(HazardControl hazardControl, string mappingScheme)
        {
            hazardControlMappingSchemeRegistrator.Register();
            var updatedHazardControl = unitOfWork.Add(hazardControl, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedHazardControl;
        }

        public IQueryable<HazardControl> GetHazardControlsQuery(Expression<Func<HazardControl, bool>> expression = null, params Expression<Func<HazardControl, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<HazardControl, bool>> GetHazardControlExpression(HazardControlFilter filter)
        {
            Expression<Func<HazardControl, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = x => x.Id == filter.Id;
            }
            return expression;
        }
    }
}
