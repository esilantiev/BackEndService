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
using Ises.Domain.HazardGroups;

namespace Ises.Data.Repositories
{
    public interface IHazardGroupRepository : IRepository
    {
        Task<PagedResult<HazardGroup>> GetHazardGroupsAsync(HazardGroupFilter hazardGroupFilter);
        Task<long> CreateHazardGroupAsync(HazardGroup hazardGroup, string mappingScheme);
        Task RemoveHazardGroupAsync(long id);
        Task<HazardGroup> UpdateHazardGroupAsync(HazardGroup hazardGroup, string mappingScheme);

        IQueryable<HazardGroup> GetHazardGroupsQuery(Expression<Func<HazardGroup, bool>> expression = null, params Expression<Func<HazardGroup, object>>[] includes);
    }

    public class HazardGroupRepository : BaseRepository, IHazardGroupRepository
    {
        readonly IUnitOfWork unitOfWork;
        private IHazardGroupMappingSchemeRegistrator hazardGroupMappingSchemeRegistrator;

        public HazardGroupRepository(IUnitOfWork unitOfWork, IHazardGroupMappingSchemeRegistrator hazardGroupMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.hazardGroupMappingSchemeRegistrator = hazardGroupMappingSchemeRegistrator;
        }

        public async Task<PagedResult<HazardGroup>> GetHazardGroupsAsync(HazardGroupFilter filter)
        {
            filter = filter ?? new HazardGroupFilter();

            var result = unitOfWork.Query(GetInstallationExpression(filter), filter.PropertiesToInclude);

            List<HazardGroup> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<HazardGroup>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetInstallationExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreateHazardGroupAsync(HazardGroup hazardGroup, string mappingScheme)
        {
            hazardGroupMappingSchemeRegistrator.Register();
            var insertedHazardGroup = unitOfWork.Add(hazardGroup, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedHazardGroup.Id;
        }

        public async Task RemoveHazardGroupAsync(long id)
        {
            var hazardGroup = await unitOfWork.Query<HazardGroup>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(hazardGroup);
            await unitOfWork.SaveAsync();
        }

        public async Task<HazardGroup> UpdateHazardGroupAsync(HazardGroup hazardGroup, string mappingScheme)
        {
            hazardGroupMappingSchemeRegistrator.Register();
            var updatedHazardGroup = unitOfWork.Add(hazardGroup, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedHazardGroup;
        }

        public IQueryable<HazardGroup> GetHazardGroupsQuery(Expression<Func<HazardGroup, bool>> expression = null, params Expression<Func<HazardGroup, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<HazardGroup, bool>> GetInstallationExpression(HazardGroupFilter filter)
        {
            Expression<Func<HazardGroup, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = x => x.Id == filter.Id;
            }
            return expression;
        }
    }
}
