using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ises.Contracts.ClientFilters;
using Ises.Core.Common;
using Ises.Core.Infrastructure;

namespace Ises.Data.Repositories
{
    public class BaseRepository
    {
        protected readonly IUnitOfWork unitOfWork;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected async Task<PageInfo> GetPageInfo<T>(Expression<Func<T, long>> selectClause, Filter filter, Expression<Func<T, bool>> whereClause = null) where T : class
        {
            var pageInfo = new PageInfo()
            {
                CurrentPage = 1,
                PageSize = 1,
                TotalItems = 1
            };
            if (filter.ApplyPaging)
            {

                int totalItems = await unitOfWork.Query(whereClause).Select(selectClause).CountAsync();
                pageInfo = new PageInfo
                {
                    CurrentPage = filter.Page,
                    PageSize = filter.PageSize,
                    TotalItems = totalItems,
                };
            }
            return pageInfo;
        }

        protected async Task<PageInfo> GetPageInfo<T>(IQueryable<T> query, Filter filter, Expression<Func<T, bool>> whereClause = null) where T : class
        {
            var pageInfo = new PageInfo()
            {
                CurrentPage = 1,
                PageSize = 1,
                TotalItems = 1
            };
            if (filter.ApplyPaging)
            {

                int totalItems = await query.CountAsync();
                pageInfo = new PageInfo
                {
                    CurrentPage = filter.Page,
                    PageSize = filter.PageSize,
                    TotalItems = totalItems,
                };
            }
            return pageInfo;
        }
    }
}
