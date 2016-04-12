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
using Ises.Domain.IsolationCertificates;

namespace Ises.Data.Repositories
{
    public interface IIsolationCertificateRepository : IRepository
    {
        Task<PagedResult<IsolationCertificate>> GetIsolationCertificatesAsync(IsolationCertificateFilter filter);
        Task<long> CreateIsolationCertificateAsync(IsolationCertificate isolationCertificate, string mappingScheme);
        Task RemoveIsolationCertificateAsync(long id);
        Task<long> UpdateIsolationCertificateAsync(IsolationCertificate isolationCertificate, string mappingScheme);

        IQueryable<IsolationCertificate> GetIsolationCertificatesQuery(Expression<Func<IsolationCertificate, bool>> expression = null, params Expression<Func<IsolationCertificate, object>>[] includes);
    }

    public class IsolationCertificateRepository : BaseRepository, IIsolationCertificateRepository
    {
        readonly IUnitOfWork unitOfWork;
        private IIsolationCertificateMappingSchemeRegistrator isolationCertificateMappingSchemeRegistrator;

        public IsolationCertificateRepository(IUnitOfWork unitOfWork, IIsolationCertificateMappingSchemeRegistrator isolationCertificateMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.isolationCertificateMappingSchemeRegistrator = isolationCertificateMappingSchemeRegistrator;
        }

        public async Task<PagedResult<IsolationCertificate>> GetIsolationCertificatesAsync(IsolationCertificateFilter filter)
        {
            filter = filter ?? new IsolationCertificateFilter();

            var result = unitOfWork.Query(GetIsolationCertificateExpression(filter), filter.PropertiesToInclude);
      
            List<IsolationCertificate> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<IsolationCertificate>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetIsolationCertificateExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreateIsolationCertificateAsync(IsolationCertificate isolationCertificate, string mappingScheme)
        {
            isolationCertificateMappingSchemeRegistrator.Register();
            var insertedIsolationCertificate = unitOfWork.Add(isolationCertificate, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedIsolationCertificate.Id;
        }

        public async Task RemoveIsolationCertificateAsync(long id)
        {
            var isolationCertificate = await unitOfWork.Query<IsolationCertificate>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(isolationCertificate);
            await unitOfWork.SaveAsync();
        }

        public async Task<long> UpdateIsolationCertificateAsync(IsolationCertificate isolationCertificate, string mappingScheme)
        {
            isolationCertificateMappingSchemeRegistrator.Register();
            var updatedIsolationCertificate = unitOfWork.Add(isolationCertificate, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedIsolationCertificate.Id;
        }

        public IQueryable<IsolationCertificate> GetIsolationCertificatesQuery(Expression<Func<IsolationCertificate, bool>> expression = null, params Expression<Func<IsolationCertificate, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<IsolationCertificate, bool>> GetIsolationCertificateExpression(IsolationCertificateFilter filter)
        {
            Expression<Func<IsolationCertificate, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = x => x.Id == filter.Id;
            }
            return expression;
        }
    }


}
