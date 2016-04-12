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
using Ises.Domain.HistoryChanges;

namespace Ises.Data.Repositories
{
    public interface IHistoryChangeRepository : IRepository
    {
        Task<PagedResult<HistoryChange>> GetHistoryChangesAsync(HistoryChangeFilter filter);
        Task<long> CreateHistoryChangeAsync(HistoryChange historyChange, string mappingScheme);
        Task RemoveHistoryChangeAsync(long id);
        Task<long> UpdateHistoryChangeAsync(HistoryChange historyChange, string mappingScheme);

        IQueryable<HistoryChange> GetHistoryChangesQuery(Expression<Func<HistoryChange, bool>> expression = null, params Expression<Func<HistoryChange, object>>[] includes);
    }

    public class HistoryChangeRepository : BaseRepository, IHistoryChangeRepository
    {
        readonly IUnitOfWork unitOfWork;
        private IHistoryChangeMappingSchemeRegistrator historyChangeMappingSchemeRegistrator;

        public HistoryChangeRepository(IUnitOfWork unitOfWork, IHistoryChangeMappingSchemeRegistrator historyChangeMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.historyChangeMappingSchemeRegistrator = historyChangeMappingSchemeRegistrator;
        }

        public async Task<PagedResult<HistoryChange>> GetHistoryChangesAsync(HistoryChangeFilter filter)
        {
            filter = filter ?? new HistoryChangeFilter();

            var result = unitOfWork.Query(GetHistoryChangeExpression(filter), filter.PropertiesToInclude);
      
            List<HistoryChange> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<HistoryChange>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetHistoryChangeExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreateHistoryChangeAsync(HistoryChange historyChange, string mappingScheme)
        {
            historyChangeMappingSchemeRegistrator.Register();
            var insertedHistoryChange = unitOfWork.Add(historyChange, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedHistoryChange.Id;
        }

        public async Task RemoveHistoryChangeAsync(long id)
        {
            var historyChange = await unitOfWork.Query<HistoryChange>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(historyChange);
            await unitOfWork.SaveAsync();
        }

        public async Task<long> UpdateHistoryChangeAsync(HistoryChange historyChange, string mappingScheme)
        {
            historyChangeMappingSchemeRegistrator.Register();
            var updatedHistoryChange = unitOfWork.Add(historyChange, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedHistoryChange.Id;
        }

        public IQueryable<HistoryChange> GetHistoryChangesQuery(Expression<Func<HistoryChange, bool>> expression = null, params Expression<Func<HistoryChange, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<HistoryChange, bool>> GetHistoryChangeExpression(HistoryChangeFilter filter)
        {
            Expression<Func<HistoryChange, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = x => x.Id == filter.Id;
            }
            return expression;
        }
    }


}
