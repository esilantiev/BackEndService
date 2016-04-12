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
using Ises.Domain.Users;

namespace Ises.Data.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task<PagedResult<User>> GetUsersAsync(UserFilter filter);
        Task<long> CreateUserAsync(User user, string mappingScheme);
        Task RemoveUserAsync(long id);
        Task<long> UpdateUserAsync(User user, string mappingScheme);

        IQueryable<User> GetUsersQuery(Expression<Func<User, bool>> expression = null, params Expression<Func<User, object>>[] includes);
    }

    public class UserRepository : BaseRepository, IUserRepository
    {
        readonly IUnitOfWork unitOfWork;
        private IUserMappingSchemeRegistrator userMappingSchemeRegistrator;

        public UserRepository(IUnitOfWork unitOfWork, IUserMappingSchemeRegistrator userMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.userMappingSchemeRegistrator = userMappingSchemeRegistrator;
        }

        public async Task<PagedResult<User>> GetUsersAsync(UserFilter filter)
        {
            filter = filter ?? new UserFilter();

            var result = unitOfWork.Query(GetUserExpression(filter), filter.PropertiesToInclude);
      
            List<User> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<User>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetUserExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreateUserAsync(User user, string mappingScheme)
        {
            userMappingSchemeRegistrator.Register();
            var insertedUser = unitOfWork.Add(user, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedUser.Id;
        }

        public async Task RemoveUserAsync(long id)
        {
            var user = await unitOfWork.Query<User>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(user);
            await unitOfWork.SaveAsync();
        }

        public async Task<long> UpdateUserAsync(User user, string mappingScheme)
        {
            userMappingSchemeRegistrator.Register();
            var updatedUser = unitOfWork.Add(user, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedUser.Id;
        }

        public IQueryable<User> GetUsersQuery(Expression<Func<User, bool>> expression = null, params Expression<Func<User, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<User, bool>> GetUserExpression(UserFilter filter)
        {
            Expression<Func<User, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = x => x.Id == filter.Id;
            }
            return expression;
        }
    }


}
