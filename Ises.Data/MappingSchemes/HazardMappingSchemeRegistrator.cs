using Ises.Domain.Hazards;
using RefactorThis.GraphDiff;
using RefactorThis.GraphDiff.Aggregates;

namespace Ises.Data.MappingSchemes
{
    public class HazardMappingSchemeRegistrator : IHazardMappingSchemeRegistrator
    {
        public void Register()
        {
            AggregateConfiguration.Aggregates
                .Register<Hazard>("Hazard", null);

            AggregateConfiguration.Aggregates
                .Register<Hazard>("FullGraph", configuration => configuration
                                    .OwnedCollection(hazard => hazard.HazardControls));
        }
    }
}
