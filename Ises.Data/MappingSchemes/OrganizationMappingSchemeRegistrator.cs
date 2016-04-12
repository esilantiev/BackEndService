using Ises.Domain.Organizations;
using RefactorThis.GraphDiff;
using RefactorThis.GraphDiff.Aggregates;

namespace Ises.Data.MappingSchemes
{
    public class OrganizationMappingSchemeRegistrator : IOrganizationMappingSchemeRegistrator
    {
        public void Register()
        {
            AggregateConfiguration.Aggregates
                .Register<Organization>("Organization", null);

            AggregateConfiguration.Aggregates
                .Register<Organization>("FullGraph", configuration => configuration
                                    .OwnedCollection(organization => organization.LeadDisciplines));
        }
    }
}
