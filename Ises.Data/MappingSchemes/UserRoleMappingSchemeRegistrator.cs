using Ises.Domain.UserRoles;
using RefactorThis.GraphDiff;
using RefactorThis.GraphDiff.Aggregates;

namespace Ises.Data.MappingSchemes
{
    public class UserRoleMappingSchemeRegistrator : IUserRoleMappingSchemeRegistrator
    {
        public void Register()
        {
            AggregateConfiguration.Aggregates
                .Register<UserRole>("UserRole", null);

            AggregateConfiguration.Aggregates
                .Register<UserRole>("FullGraph", configuration => configuration
                                    .AssociatedCollection(user => user.RolePermissions));
        }
    }
}
