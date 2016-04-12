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
using Ises.Domain.UserRoles;

namespace Ises.Data.Repositories
{
    public interface IUserRoleRepository : IRepository
    {
        Task<PagedResult<UserRole>> GetUserRolesAsync(UserRoleFilter filter);
        Task<long> CreateUserRoleAsync(UserRole userRole, string mappingScheme);
        Task RemoveUserRoleAsync(long id);
        Task<long> UpdateUserRoleAsync(UserRole userRole, string mappingScheme);

        IQueryable<UserRole> GetUserRolesQuery(Expression<Func<UserRole, bool>> expression = null, params Expression<Func<UserRole, object>>[] includes);
    }

    public class UserRoleRepository : BaseRepository, IUserRoleRepository
    {
        readonly IUnitOfWork unitOfWork;
        private IUserRoleMappingSchemeRegistrator userRoleMappingSchemeRegistrator;

        public UserRoleRepository(IUnitOfWork unitOfWork, IUserRoleMappingSchemeRegistrator userRoleMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.userRoleMappingSchemeRegistrator = userRoleMappingSchemeRegistrator;
        }

        public async Task<PagedResult<UserRole>> GetUserRolesAsync(UserRoleFilter filter)
        {
            filter = filter ?? new UserRoleFilter();

            var result = unitOfWork.Query(GetUserRoleExpression(filter), filter.PropertiesToInclude);

            List<UserRole> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<UserRole>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetUserRoleExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreateUserRoleAsync(UserRole userRole, string mappingScheme)
        {
            userRoleMappingSchemeRegistrator.Register();
            var insertedUserRole = unitOfWork.Add(userRole, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedUserRole.Id;
        }

        public async Task RemoveUserRoleAsync(long id)
        {
            var userRole = await unitOfWork.Query<UserRole>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(userRole);
            await unitOfWork.SaveAsync();
        }

        public async Task<long> UpdateUserRoleAsync(UserRole userRole, string mappingScheme)
        {
            userRoleMappingSchemeRegistrator.Register();
            var updatedUserRole = unitOfWork.Add(userRole, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedUserRole.Id;
        }

        public IQueryable<UserRole> GetUserRolesQuery(Expression<Func<UserRole, bool>> expression = null, params Expression<Func<UserRole, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<UserRole, bool>> GetUserRoleExpression(UserRoleFilter filter)
        {
            Expression<Func<UserRole, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = x => x.Id == filter.Id;
            }
            return expression;
        }
    }


}
