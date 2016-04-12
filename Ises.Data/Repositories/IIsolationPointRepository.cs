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
using Ises.Domain.IsolationPoints;

namespace Ises.Data.Repositories
{
    public interface IIsolationPointRepository : IRepository
    {
        Task<PagedResult<IsolationPoint>> GetIsolationPointsAsync(IsolationPointFilter filter);
        Task<long> CreateIsolationPointAsync(IsolationPoint isolationPoint, string mappingScheme);
        Task RemoveIsolationPointAsync(long id);
        Task<long> UpdateIsolationPointAsync(IsolationPoint isolationPoint, string mappingScheme);

        IQueryable<IsolationPoint> GetIsolationPointsQuery(Expression<Func<IsolationPoint, bool>> expression = null, params Expression<Func<IsolationPoint, object>>[] includes);
    }

    public class IsolationPointRepository : BaseRepository, IIsolationPointRepository
    {
        readonly IUnitOfWork unitOfWork;
        private IIsolationPointMappingSchemeRegistrator isolationPointMappingSchemeRegistrator;

        public IsolationPointRepository(IUnitOfWork unitOfWork, IIsolationPointMappingSchemeRegistrator isolationPointMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.isolationPointMappingSchemeRegistrator = isolationPointMappingSchemeRegistrator;
        }

        public async Task<PagedResult<IsolationPoint>> GetIsolationPointsAsync(IsolationPointFilter filter)
        {
            filter = filter ?? new IsolationPointFilter();

            var result = unitOfWork.Query(GetIsolationPointExpression(filter), filter.PropertiesToInclude);
      
            List<IsolationPoint> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<IsolationPoint>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetIsolationPointExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreateIsolationPointAsync(IsolationPoint isolationPoint, string mappingScheme)
        {
            isolationPointMappingSchemeRegistrator.Register();
            var insertedIsolationPoint = unitOfWork.Add(isolationPoint, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedIsolationPoint.Id;
        }

        public async Task RemoveIsolationPointAsync(long id)
        {
            var isolationPoint = await unitOfWork.Query<IsolationPoint>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(isolationPoint);
            await unitOfWork.SaveAsync();
        }

        public async Task<long> UpdateIsolationPointAsync(IsolationPoint isolationPoint, string mappingScheme)
        {
            isolationPointMappingSchemeRegistrator.Register();
            var updatedIsolationPoint = unitOfWork.Add(isolationPoint, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedIsolationPoint.Id;
        }

        public IQueryable<IsolationPoint> GetIsolationPointsQuery(Expression<Func<IsolationPoint, bool>> expression = null, params Expression<Func<IsolationPoint, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<IsolationPoint, bool>> GetIsolationPointExpression(IsolationPointFilter filter)
        {
            Expression<Func<IsolationPoint, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = x => x.Id == filter.Id;
            }
            return expression;
        }
    }


}
