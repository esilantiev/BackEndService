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
using Ises.Domain.AreaTypes;

namespace Ises.Data.Repositories
{
    public interface IAreaTypeRepository : IRepository
    {
        Task<PagedResult<AreaType>> GetAreaTypesAsync(AreaTypeFilter filter);
        Task<long> CreateAreaTypeAsync(AreaType areaType, string mappingScheme);
        Task RemoveAreaTypeAsync(long id);
        Task<AreaType> UpdateAreaTypeAsync(AreaType areaType, string mappingScheme);

        IQueryable<AreaType> GetAreaTypesQuery(Expression<Func<AreaType, bool>> expression = null, params Expression<Func<AreaType, object>>[] includes);
    }

    public class AreaTypeRepository : BaseRepository, IAreaTypeRepository
    {
        readonly IUnitOfWork unitOfWork;
        private IAreaTypeMappingSchemeRegistrator areaTypeMappingSchemeRegistrator;

        public AreaTypeRepository(IUnitOfWork unitOfWork, IAreaTypeMappingSchemeRegistrator areaTypeMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.areaTypeMappingSchemeRegistrator = areaTypeMappingSchemeRegistrator;
        }

        public async Task<PagedResult<AreaType>> GetAreaTypesAsync(AreaTypeFilter filter)
        {
            filter = filter ?? new AreaTypeFilter();

            var result = unitOfWork.Query(GetAreaTypeExpression(filter), filter.PropertiesToInclude);

            List<AreaType> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<AreaType>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetAreaTypeExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreateAreaTypeAsync(AreaType areaType, string mappingScheme)
        {
            areaTypeMappingSchemeRegistrator.Register();
            var insertedAreaType = unitOfWork.Add(areaType, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedAreaType.Id;
        }

        public async Task RemoveAreaTypeAsync(long id)
        {
            var areaType = await unitOfWork.Query<AreaType>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(areaType);
            await unitOfWork.SaveAsync();
        }

        public async Task<AreaType> UpdateAreaTypeAsync(AreaType areaType, string mappingScheme)
        {
            areaTypeMappingSchemeRegistrator.Register();
            var updatedAreaType = unitOfWork.Add(areaType, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedAreaType;
        }

        public IQueryable<AreaType> GetAreaTypesQuery(Expression<Func<AreaType, bool>> expression = null, params Expression<Func<AreaType, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<AreaType, bool>> GetAreaTypeExpression(AreaTypeFilter filter)
        {
            Expression<Func<AreaType, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = x => x.Id == filter.Id;
            }
            return expression;
        }
    }


}
