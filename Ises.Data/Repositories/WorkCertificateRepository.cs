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
using Ises.Domain.WorkCertificates;

namespace Ises.Data.Repositories
{
    public interface IWorkCertificateRepository : IRepository
    {
        Task<PagedResult<WorkCertificate>> GetWorkCertificatesAsync(WorkCertificateFilter filter);
        Task<long> CreateWorkCertificateAsync(WorkCertificate workCertificate, string mappingScheme);
        Task RemoveWorkCertificateAsync(long id);
        Task<long> UpdateWorkCertificateAsync(WorkCertificate workCertificate, string mappingScheme);

        IQueryable<WorkCertificate> GetWorkCertificatesQuery(Expression<Func<WorkCertificate, bool>> expression = null, params Expression<Func<WorkCertificate, object>>[] includes);
    }

    public class WorkCertificateRepository : BaseRepository, IWorkCertificateRepository
    {
        readonly IUnitOfWork unitOfWork;
        private IWorkCertificateMappingSchemeRegistrator workCertificateMappingSchemeRegistrator;

        public WorkCertificateRepository(IUnitOfWork unitOfWork, IWorkCertificateMappingSchemeRegistrator workCertificateMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.workCertificateMappingSchemeRegistrator = workCertificateMappingSchemeRegistrator;
        }

        public async Task<PagedResult<WorkCertificate>> GetWorkCertificatesAsync(WorkCertificateFilter filter)
        {
            filter = filter ?? new WorkCertificateFilter();

            var result = unitOfWork.Query(GetWorkCertificateExpression(filter), filter.PropertiesToInclude);
      
            List<WorkCertificate> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<WorkCertificate>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetWorkCertificateExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreateWorkCertificateAsync(WorkCertificate workCertificate, string mappingScheme)
        {
            workCertificateMappingSchemeRegistrator.Register();
            var insertedWorkCertificate = unitOfWork.Add(workCertificate, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedWorkCertificate.Id;
        }

        public async Task RemoveWorkCertificateAsync(long id)
        {
            var workCertificate = await unitOfWork.Query<WorkCertificate>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(workCertificate);
            await unitOfWork.SaveAsync();
        }

        public async Task<long> UpdateWorkCertificateAsync(WorkCertificate workCertificate, string mappingScheme)
        {
            workCertificateMappingSchemeRegistrator.Register();
            var updatedWorkCertificate = unitOfWork.Add(workCertificate, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedWorkCertificate.Id;
        }

        public IQueryable<WorkCertificate> GetWorkCertificatesQuery(Expression<Func<WorkCertificate, bool>> expression = null, params Expression<Func<WorkCertificate, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<WorkCertificate, bool>> GetWorkCertificateExpression(WorkCertificateFilter filter)
        {
            Expression<Func<WorkCertificate, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = x => x.Id == filter.Id;
            }
            return expression;
        }
    }


}
