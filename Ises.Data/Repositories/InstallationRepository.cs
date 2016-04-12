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
using Ises.Domain.Installations;

namespace Ises.Data.Repositories
{
    public interface IInstallationRepository : IRepository
    {
        Task<PagedResult<Installation>> GetInstallationsAsync(InstallationFilter installationFilter);
        Task<long> CreateInstallationAsync(Installation installation, string mappingScheme);
        Task RemoveInstallationAsync(long id);
        Task<Installation> UpdateInstallationAsync(Installation installation, string mappingScheme);

        IQueryable<Installation> GetInstallationsQuery(Expression<Func<Installation, bool>> expression = null, params Expression<Func<Installation, object>>[] includes);
    }

    public class InstallationRepository : BaseRepository, IInstallationRepository
    {
        readonly IUnitOfWork unitOfWork;
        private IInstallationMappingSchemeRegistrator installationMappingSchemeRegistrator;

        public InstallationRepository(IUnitOfWork unitOfWork, IInstallationMappingSchemeRegistrator installationMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.installationMappingSchemeRegistrator = installationMappingSchemeRegistrator;
        }

        public async Task<PagedResult<Installation>> GetInstallationsAsync(InstallationFilter filter)
        {
            filter = filter ?? new InstallationFilter();

            var result = unitOfWork.Query(GetInstallationExpression(filter), filter.PropertiesToInclude);

            List<Installation> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<Installation>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetInstallationExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreateInstallationAsync(Installation installation, string mappingScheme)
        {
            installationMappingSchemeRegistrator.Register();
            var insertedInstallation = unitOfWork.Add(installation, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedInstallation.Id;
        }

        public async Task RemoveInstallationAsync(long id)
        {
            var installation = await unitOfWork.Query<Installation>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(installation);
            await unitOfWork.SaveAsync();
        }

        public async Task<Installation> UpdateInstallationAsync(Installation installation, string mappingScheme)
        {
            installationMappingSchemeRegistrator.Register();
            var updatedInstallation = unitOfWork.Add(installation, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedInstallation;
        }

        public IQueryable<Installation> GetInstallationsQuery(Expression<Func<Installation, bool>> expression = null, params Expression<Func<Installation, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<Installation, bool>> GetInstallationExpression(InstallationFilter filter)
        {
            Expression<Func<Installation, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = x => x.Id == filter.Id;
            }
            return expression;
        }
    }
}
