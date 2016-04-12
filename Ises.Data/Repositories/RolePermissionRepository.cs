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
using Ises.Domain.RolePermissions;

namespace Ises.Data.Repositories
{
    public interface IRolePermissionRepository : IRepository
    {
        Task<PagedResult<RolePermission>> GetRolePermissionsAsync(RolePermissionFilter filter);
        Task<long> CreateRolePermissionAsync(RolePermission rolePermission, string mappingScheme);
        Task RemoveRolePermissionAsync(long id);
        Task<long> UpdateRolePermissionAsync(RolePermission rolePermission, string mappingScheme);

        IQueryable<RolePermission> GetRolePermissionsQuery(Expression<Func<RolePermission, bool>> expression = null, params Expression<Func<RolePermission, object>>[] includes);
    }

    public class RolePermissionRepository : BaseRepository, IRolePermissionRepository
    {
        readonly IUnitOfWork unitOfWork;
        private IRolePermissionMappingSchemeRegistrator rolePermissionMappingSchemeRegistrator;

        public RolePermissionRepository(IUnitOfWork unitOfWork, IRolePermissionMappingSchemeRegistrator rolePermissionMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.rolePermissionMappingSchemeRegistrator = rolePermissionMappingSchemeRegistrator;
        }

        public async Task<PagedResult<RolePermission>> GetRolePermissionsAsync(RolePermissionFilter filter)
        {
            filter = filter ?? new RolePermissionFilter();

            var result = unitOfWork.Query(GetRolePermissionExpression(filter), filter.PropertiesToInclude);

            List<RolePermission> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<RolePermission>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetRolePermissionExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreateRolePermissionAsync(RolePermission rolePermission, string mappingScheme)
        {
            rolePermissionMappingSchemeRegistrator.Register();
            var insertedRolePermission = unitOfWork.Add(rolePermission, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedRolePermission.Id;
        }

        public async Task RemoveRolePermissionAsync(long id)
        {
            var rolePermission = await unitOfWork.Query<RolePermission>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(rolePermission);
            await unitOfWork.SaveAsync();
        }

        public async Task<long> UpdateRolePermissionAsync(RolePermission rolePermission, string mappingScheme)
        {
            rolePermissionMappingSchemeRegistrator.Register();
            var updatedRolePermission = unitOfWork.Add(rolePermission, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedRolePermission.Id;
        }

        public IQueryable<RolePermission> GetRolePermissionsQuery(Expression<Func<RolePermission, bool>> expression = null, params Expression<Func<RolePermission, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<RolePermission, bool>> GetRolePermissionExpression(RolePermissionFilter filter)
        {
            Expression<Func<RolePermission, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = x => x.Id == filter.Id;
            }
            return expression;
        }
    }


}
