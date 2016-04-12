using Ises.Domain.Users;
using RefactorThis.GraphDiff;
using RefactorThis.GraphDiff.Aggregates;

namespace Ises.Data.MappingSchemes
{
    public class UserMappingSchemeRegistrator: IUserMappingSchemeRegistrator
    {
        public void Register()
        {
            AggregateConfiguration.Aggregates
                .Register<User>("User", null);

            AggregateConfiguration.Aggregates
                .Register<User>("FullGraph", configuration => configuration
                                    .AssociatedCollection(user => user.UserRoles));
        }
    }
}
