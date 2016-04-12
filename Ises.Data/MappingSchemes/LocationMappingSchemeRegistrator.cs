using Ises.Domain.Locations;
using RefactorThis.GraphDiff;
using RefactorThis.GraphDiff.Aggregates;

namespace Ises.Data.MappingSchemes
{
    public class LocationMappingSchemeRegistrator : ILocationMappingSchemeRegistrator
    {
        public void Register()
        {
            AggregateConfiguration.Aggregates
                .Register<Location>("Location", null);

            AggregateConfiguration.Aggregates
                .Register<Location>("FullGraph", configuration => configuration
                                    .OwnedCollection(location => location.Areas));
        }
    }
}
