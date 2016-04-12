using Ises.Domain.RolePermissions;
using RefactorThis.GraphDiff;
using RefactorThis.GraphDiff.Aggregates;

namespace Ises.Data.MappingSchemes
{
    public class RolePermissionMappingSchemeRegistrator : IRolePermissionMappingSchemeRegistrator
    {
        public void Register()
        {
            AggregateConfiguration.Aggregates
               .Register<RolePermission>("RolePermission", null);

            AggregateConfiguration.Aggregates
                .Register<RolePermission>("FullGraph", configuration => configuration
                                    .AssociatedCollection(rolePermission => rolePermission.UserRoles));
        }
    }
}
