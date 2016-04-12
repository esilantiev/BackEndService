using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.RolePermissionsDto;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.RolePermissions;

namespace Ises.Application.Managers
{
    public interface IRolePermissionManager : IManager
    {
        Task<PagedResult<RolePermissionDto>> GetRolePermissionsAsync(RolePermissionFilter rolePermissionFilter);
        Task RemoveRolePermissionAsync(long id);
        Task<long> CreateRolePermissionAsync(RolePermissionDto rolePermissionDto);
        Task<long> UpdateRolePermissionAsync(RolePermissionDto rolePermissionDto);
    }

    public class RolePermissionManager : IRolePermissionManager
    {
        readonly IRolePermissionRepository rolePermissionRepository;

        public RolePermissionManager(IRolePermissionRepository rolePermissionRepository)
        {
            this.rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<PagedResult<RolePermissionDto>> GetRolePermissionsAsync(RolePermissionFilter rolePermissionFilter)
        {
            var rolePermissionsPagedResult = await rolePermissionRepository.GetRolePermissionsAsync(rolePermissionFilter);

            var rolePermissionsModelPagedResult = new PagedResult<RolePermissionDto>();
            Mapper.Map(rolePermissionsPagedResult, rolePermissionsModelPagedResult);
            return rolePermissionsModelPagedResult;
        }

        public Task RemoveRolePermissionAsync(long id)
        {
            return rolePermissionRepository.RemoveRolePermissionAsync(id);
        }

        public async Task<long> CreateRolePermissionAsync(RolePermissionDto rolePermissionDto)
        {
            var rolePermission = new RolePermission();
            Mapper.Map(rolePermissionDto, rolePermission);
            var rowsUpdated = await rolePermissionRepository.CreateRolePermissionAsync(rolePermission, rolePermissionDto.MappingScheme);
            return rowsUpdated;
        }

        public async Task<long> UpdateRolePermissionAsync(RolePermissionDto rolePermissionDto)
        {
            var rolePermission = new RolePermission();
            Mapper.Map(rolePermissionDto, rolePermission);
            var rowsUpdated = await rolePermissionRepository.UpdateRolePermissionAsync(rolePermission, rolePermissionDto.MappingScheme);
            return rowsUpdated;
        }

    }
}