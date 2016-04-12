using Ises.Domain.HazardGroups;
using RefactorThis.GraphDiff;
using RefactorThis.GraphDiff.Aggregates;

namespace Ises.Data.MappingSchemes
{
    public class HazardGroupMappingSchemeRegistrator : IHazardGroupMappingSchemeRegistrator
    {
        public void Register()
        {
            AggregateConfiguration.Aggregates
                .Register<HazardGroup>("HazardGroup", null);

            AggregateConfiguration.Aggregates
                .Register<HazardGroup>("FullGraph", configuration => configuration
                                    .OwnedCollection(hazardGroup => hazardGroup.Hazards));
        }
    }
}
