using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.UsersDto;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.Users;

namespace Ises.Application.Managers
{
    public interface IUserManager : IManager
    {
        Task<PagedResult<UserDto>> GetUsersAsync(UserFilter userFilter);
        Task RemoveUserAsync(long id);
        Task<long> CreateUserAsync(UserDto userDto);
        Task<long> UpdateUserAsync(UserDto userDto);
    }

    public class UserManager : IUserManager
    {
        readonly IUserRepository userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<PagedResult<UserDto>> GetUsersAsync(UserFilter userFilter)
        {
            var usersPagedResult = await userRepository.GetUsersAsync(userFilter);

            var usersModelPagedResult = new PagedResult<UserDto>();
            Mapper.Map(usersPagedResult, usersModelPagedResult);
            return usersModelPagedResult;
        }

        public Task RemoveUserAsync(long id)
        {
            return userRepository.RemoveUserAsync(id);
        }

        public async Task<long> CreateUserAsync(UserDto userDto)
        {
            var user = new User();
            Mapper.Map(userDto, user);
            var rowsUpdated = await userRepository.CreateUserAsync(user, userDto.MappingScheme);
            return rowsUpdated;
        }

        public async Task<long> UpdateUserAsync(UserDto userDto)
        {
            var user = new User();
            Mapper.Map(userDto, user);
            var rowsUpdated = await userRepository.UpdateUserAsync(user, userDto.MappingScheme);
            return rowsUpdated;
        }

    }
}