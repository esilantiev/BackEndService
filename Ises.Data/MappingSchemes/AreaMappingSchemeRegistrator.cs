using Ises.Domain.Areas;
using RefactorThis.GraphDiff;
using RefactorThis.GraphDiff.Aggregates;

namespace Ises.Data.MappingSchemes
{
    public class AreaMappingSchemeRegistrator : IAreaMappingSchemeRegistrator
    {
        public void Register()
        {
            AggregateConfiguration.Aggregates
                .Register<Area>("Area", null);

            AggregateConfiguration.Aggregates
                .Register<Area>("FullGraph", configuration => configuration
                                    .AssociatedEntity(area => area.AreaType));
        }
    }
}
