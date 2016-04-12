using Ises.Domain.HazardControls;
using RefactorThis.GraphDiff;
using RefactorThis.GraphDiff.Aggregates;

namespace Ises.Data.MappingSchemes
{
    public class HazardControlMappingSchemeRegistrator : IHazardControlMappingSchemeRegistrator
    {
        public void Register()
        {
            AggregateConfiguration.Aggregates
                .Register<HazardControl>("HazardControl", null);

            AggregateConfiguration.Aggregates
                .Register<HazardControl>("FullGraph", configuration => configuration
                                    .AssociatedCollection(hazardControl => hazardControl.HazardRules)
                                    .AssociatedCollection(hazardControl => hazardControl.WorkCertificatesHazardControls));
        }
    }
}
