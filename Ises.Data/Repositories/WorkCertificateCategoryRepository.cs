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
using Ises.Domain.WorkCertificateCategories;

namespace Ises.Data.Repositories
{
    public interface IWorkCertificateCategoryRepository : IRepository
    {
        Task<PagedResult<WorkCertificateCategory>> GetWorkCertificateCategoriesAsync(WorkCertificateCategoryFilter filter);
        Task<long> CreateWorkCertificateCategoryAsync(WorkCertificateCategory workCertificateCategory, string mappingScheme);
        Task RemoveWorkCertificateCategoryAsync(long id);
        Task<long> UpdateWorkCertificateCategoryAsync(WorkCertificateCategory workCertificateCategory, string mappingScheme);

        IQueryable<WorkCertificateCategory> GetWorkCertificateCategorysQuery(Expression<Func<WorkCertificateCategory, bool>> expression = null, params Expression<Func<WorkCertificateCategory, object>>[] includes);
    }

    public class WorkCertificateCategoryRepository : BaseRepository, IWorkCertificateCategoryRepository
    {
        readonly IUnitOfWork unitOfWork;
        private IWorkCertificateCategoryMappingSchemeRegistrator workCertificateCategoryMappingSchemeRegistrator;

        public WorkCertificateCategoryRepository(IUnitOfWork unitOfWork, IWorkCertificateCategoryMappingSchemeRegistrator workCertificateCategoryMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.workCertificateCategoryMappingSchemeRegistrator = workCertificateCategoryMappingSchemeRegistrator;
        }

        public async Task<PagedResult<WorkCertificateCategory>> GetWorkCertificateCategoriesAsync(WorkCertificateCategoryFilter filter)
        {
            filter = filter ?? new WorkCertificateCategoryFilter();

            var result = unitOfWork.Query(GetWorkCertificateCategoryExpression(filter), filter.PropertiesToInclude);
      
            List<WorkCertificateCategory> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<WorkCertificateCategory>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetWorkCertificateCategoryExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreateWorkCertificateCategoryAsync(WorkCertificateCategory workCertificateCategory, string mappingScheme)
        {
            workCertificateCategoryMappingSchemeRegistrator.Register();
            var insertedWorkCertificateCategory = unitOfWork.Add(workCertificateCategory, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedWorkCertificateCategory.Id;
        }

        public async Task RemoveWorkCertificateCategoryAsync(long id)
        {
            var workCertificateCategory = await unitOfWork.Query<WorkCertificateCategory>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(workCertificateCategory);
            await unitOfWork.SaveAsync();
        }

        public async Task<long> UpdateWorkCertificateCategoryAsync(WorkCertificateCategory workCertificateCategory, string mappingScheme)
        {
            workCertificateCategoryMappingSchemeRegistrator.Register();
            var updatedWorkCertificateCategory = unitOfWork.Add(workCertificateCategory, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedWorkCertificateCategory.Id;
        }

        public IQueryable<WorkCertificateCategory> GetWorkCertificateCategorysQuery(Expression<Func<WorkCertificateCategory, bool>> expression = null, params Expression<Func<WorkCertificateCategory, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<WorkCertificateCategory, bool>> GetWorkCertificateCategoryExpression(WorkCertificateCategoryFilter filter)
        {
            Expression<Func<WorkCertificateCategory, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = x => x.Id == filter.Id;
            }
            return expression;
        }
    }


}
