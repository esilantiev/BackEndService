using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ises.Contracts.ClientFilters;
using Ises.Core.Common;
using Ises.Core.Infrastructure;
using Ises.Core.Utils;
using Ises.Data.MappingSchemes;
using Ises.Domain.Areas;

namespace Ises.Data.Repositories
{
    public interface IAreaRepository : IRepository
    {
        Task<PagedResult<Area>> GetAreasAsync(AreaFilter filter);
        Task<long> CreateAreaAsync(Area area, string mappingScheme);
        Task RemoveAreaAsync(long id);
        Task<Area> UpdateAreaAsync(Area area, string mappingScheme);

        IQueryable<Area> GetAreasQuery(Expression<Func<Area, bool>> expression = null, params Expression<Func<Area, object>>[] includes);
    }

    public class AreaRepository : BaseRepository, IAreaRepository
    {
        readonly IUnitOfWork unitOfWork;
        private IAreaMappingSchemeRegistrator areaMappingSchemeRegistrator;

        public AreaRepository(IUnitOfWork unitOfWork, IAreaMappingSchemeRegistrator areaMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.areaMappingSchemeRegistrator = areaMappingSchemeRegistrator;
        }

        public async Task<PagedResult<Area>> GetAreasAsync(AreaFilter filter)
        {
            filter = filter ?? new AreaFilter();

            var result = unitOfWork.Query(GetAreaExpression(filter), filter.PropertiesToInclude);

            var complexQuery = unitOfWork.QueryLogs<Area>(includes: new List<string> { "LogDetails" });

            List<Area> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<Area>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetAreaExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreateAreaAsync(Area area, string mappingScheme)
        {
            areaMappingSchemeRegistrator.Register();
            var insertedArea = unitOfWork.Add(area, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedArea.Id;
        }

        public async Task RemoveAreaAsync(long id)
        {
            var area = await unitOfWork.Query<Area>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(area);
            await unitOfWork.SaveAsync();
        }

        public async Task<Area> UpdateAreaAsync(Area area, string mappingScheme)
        {
            areaMappingSchemeRegistrator.Register();
            var updatedArea = unitOfWork.Update(area, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedArea;
        }

        public IQueryable<Area> GetAreasQuery(Expression<Func<Area, bool>> expression = null, params Expression<Func<Area, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<Area, bool>> GetAreaExpression(AreaFilter filter)
        {
            Expression<Func<Area, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = expression.AddOrAssign(area => area.Id == filter.Id);
            }
            if (!filter.Name.IsNullOrEmpty())
            {
                expression = expression.AddOrAssign(area => area.Name == filter.Name);
            }
            return expression;
        }
    }


}
