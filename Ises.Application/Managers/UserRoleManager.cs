using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.UserRolesDto;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.UserRoles;

namespace Ises.Application.Managers
{
    public interface IUserRoleManager : IManager
    {
        Task<PagedResult<UserRoleDto>> GetUserRolesAsync(UserRoleFilter userRoleFilter);
        Task RemoveUserRoleAsync(long id);
        Task<long> CreateUserRoleAsync(UserRoleDto userRoleDto);
        Task<long> UpdateUserRoleAsync(UserRoleDto userRoleDto);
    }

    public class UserRoleManager : IUserRoleManager
    {
        readonly IUserRoleRepository userRoleRepository;

        public UserRoleManager(IUserRoleRepository userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
        }

        public async Task<PagedResult<UserRoleDto>> GetUserRolesAsync(UserRoleFilter userRoleFilter)
        {
            var userRolesPagedResult = await userRoleRepository.GetUserRolesAsync(userRoleFilter);

            var userRolesModelPagedResult = new PagedResult<UserRoleDto>();
            Mapper.Map(userRolesPagedResult, userRolesModelPagedResult);
            return userRolesModelPagedResult;
        }

        public Task RemoveUserRoleAsync(long id)
        {
            return userRoleRepository.RemoveUserRoleAsync(id);
        }

        public async Task<long> CreateUserRoleAsync(UserRoleDto userRoleDto)
        {
            var userRole = new UserRole();
            Mapper.Map(userRoleDto, userRole);
            var rowsUpdated = await userRoleRepository.CreateUserRoleAsync(userRole, userRoleDto.MappingScheme);
            return rowsUpdated;
        }

        public async Task<long> UpdateUserRoleAsync(UserRoleDto userRoleDto)
        {
            var userRole = new UserRole();
            Mapper.Map(userRoleDto, userRole);
            var rowsUpdated = await userRoleRepository.UpdateUserRoleAsync(userRole, userRoleDto.MappingScheme);
            return rowsUpdated;
        }

    }
}