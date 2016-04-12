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
using Ises.Domain.CertificatesUsers;

namespace Ises.Data.Repositories
{
    public interface ICertificateUserRepository : IRepository
    {
        Task<PagedResult<CertificateUser>> GetCertificateUsersAsync(CertificateUserFilter filter);
        Task<long> CreateCertificateUserAsync(CertificateUser certificateUser, string mappingScheme);
        Task RemoveCertificateUserAsync(long id);
        Task<long> UpdateCertificateUserAsync(CertificateUser certificateUser, string mappingScheme);

        IQueryable<CertificateUser> GetCertificateUsersQuery(Expression<Func<CertificateUser, bool>> expression = null, params Expression<Func<CertificateUser, object>>[] includes);
    }

    public class CertificateUserRepository : BaseRepository, ICertificateUserRepository
    {
        readonly IUnitOfWork unitOfWork;
        private ICertificateUserMappingSchemeRegistrator certificateUserMappingSchemeRegistrator;

        public CertificateUserRepository(IUnitOfWork unitOfWork, ICertificateUserMappingSchemeRegistrator certificateUserMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.certificateUserMappingSchemeRegistrator = certificateUserMappingSchemeRegistrator;
        }

        public async Task<PagedResult<CertificateUser>> GetCertificateUsersAsync(CertificateUserFilter filter)
        {
            filter = filter ?? new CertificateUserFilter();

            var result = unitOfWork.Query(GetCertificateUserExpression(filter), filter.PropertiesToInclude);

            List<CertificateUser> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<CertificateUser>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetCertificateUserExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreateCertificateUserAsync(CertificateUser certificateUser, string mappingScheme)
        {
            certificateUserMappingSchemeRegistrator.Register();
            var insertedCertificateUser = unitOfWork.Add(certificateUser, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedCertificateUser.Id;
        }

        public async Task RemoveCertificateUserAsync(long id)
        {
            var certificateUser = await unitOfWork.Query<CertificateUser>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(certificateUser);
            await unitOfWork.SaveAsync();
        }

        public async Task<long> UpdateCertificateUserAsync(CertificateUser certificateUser, string mappingScheme)
        {
            certificateUserMappingSchemeRegistrator.Register();
            var updatedCertificateUser = unitOfWork.Add(certificateUser, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedCertificateUser.Id;
        }

        public IQueryable<CertificateUser> GetCertificateUsersQuery(Expression<Func<CertificateUser, bool>> expression = null, params Expression<Func<CertificateUser, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<CertificateUser, bool>> GetCertificateUserExpression(CertificateUserFilter filter)
        {
            Expression<Func<CertificateUser, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = x => x.Id == filter.Id;
            }
            return expression;
        }
    }


}
