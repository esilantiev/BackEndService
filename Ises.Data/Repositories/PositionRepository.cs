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
using Ises.Domain.Positions;

namespace Ises.Data.Repositories
{
    public interface IPositionRepository : IRepository
    {
        Task<PagedResult<Position>> GetPositionsAsync(PositionFilter positionFilter);
        Task<long> CreatePositionAsync(Position position, string mappingScheme);
        Task RemovePositionAsync(long id);
        Task<long> UpdatePositionAsync(Position position, string mappingScheme);

        IQueryable<Position> GetPositionsQuery(Expression<Func<Position, bool>> expression = null, params Expression<Func<Position, object>>[] includes);
    }

    public class PositionRepository : BaseRepository, IPositionRepository
    {
        readonly IUnitOfWork unitOfWork;
        private IPositionMappingSchemeRegistrator positionMappingSchemeRegistrator;

        public PositionRepository(IUnitOfWork unitOfWork, IPositionMappingSchemeRegistrator positionMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.positionMappingSchemeRegistrator = positionMappingSchemeRegistrator;
        }

        public async Task<PagedResult<Position>> GetPositionsAsync(PositionFilter filter)
        {
            filter = filter ?? new PositionFilter();

            var result = unitOfWork.Query(GetPositionExpression(filter), filter.PropertiesToInclude);

            List<Position> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<Position>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetPositionExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreatePositionAsync(Position position, string mappingScheme)
        {
            positionMappingSchemeRegistrator.Register();
            var insertedPosition = unitOfWork.Add(position, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedPosition.Id;
        }

        public async Task RemovePositionAsync(long id)
        {
            var position = await unitOfWork.Query<Position>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(position);
            await unitOfWork.SaveAsync();
        }

        public async Task<long> UpdatePositionAsync(Position position, string mappingScheme)
        {
            positionMappingSchemeRegistrator.Register();
            var updatedPosition = unitOfWork.Add(position, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedPosition.Id;
        }

        public IQueryable<Position> GetPositionsQuery(Expression<Func<Position, bool>> expression = null, params Expression<Func<Position, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<Position, bool>> GetPositionExpression(PositionFilter filter)
        {
            Expression<Func<Position, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = x => x.Id == filter.Id;
            }
            return expression;
        }
    }
}
