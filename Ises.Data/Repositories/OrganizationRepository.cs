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
using Ises.Domain.Organizations;

namespace Ises.Data.Repositories
{
    public interface IOrganizationRepository : IRepository
    {
        Task<PagedResult<Organization>> GetOrganizationsAsync(OrganizationFilter filter);
        Task<long> CreateOrganizationAsync(Organization organization, string mappingScheme);
        Task RemoveOrganizationAsync(long id);
        Task<long> UpdateOrganizationAsync(Organization organization, string mappingScheme);

        IQueryable<Organization> GetOrganizationsQuery(Expression<Func<Organization, bool>> expression = null, params Expression<Func<Organization, object>>[] includes);
    }

    public class OrganizationRepository : BaseRepository, IOrganizationRepository
    {
        readonly IUnitOfWork unitOfWork;
        private IOrganizationMappingSchemeRegistrator organizationMappingSchemeRegistrator;

        public OrganizationRepository(IUnitOfWork unitOfWork, IOrganizationMappingSchemeRegistrator organizationMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.organizationMappingSchemeRegistrator = organizationMappingSchemeRegistrator;
        }

        public async Task<PagedResult<Organization>> GetOrganizationsAsync(OrganizationFilter filter)
        {
            filter = filter ?? new OrganizationFilter();

            var result = unitOfWork.Query(GetUserExpression(filter), filter.PropertiesToInclude);

            List<Organization> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<Organization>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetUserExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreateOrganizationAsync(Organization organization, string mappingScheme)
        {
            organizationMappingSchemeRegistrator.Register();
            var insertedOrganization = unitOfWork.Add(organization, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedOrganization.Id;
        }

        public async Task RemoveOrganizationAsync(long id)
        {
            var organization = await unitOfWork.Query<Organization>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(organization);
            await unitOfWork.SaveAsync();
        }

        public async Task<long> UpdateOrganizationAsync(Organization organization, string mappingScheme)
        {
            organizationMappingSchemeRegistrator.Register();
            var updatedOrganization = unitOfWork.Add(organization, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedOrganization.Id;
        }

        public IQueryable<Organization> GetOrganizationsQuery(Expression<Func<Organization, bool>> expression = null, params Expression<Func<Organization, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<Organization, bool>> GetUserExpression(OrganizationFilter filter)
        {
            Expression<Func<Organization, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = x => x.Id == filter.Id;
            }
            return expression;
        }
    }


}
