using Ises.Domain.AreaTypes;
using RefactorThis.GraphDiff;
using RefactorThis.GraphDiff.Aggregates;

namespace Ises.Data.MappingSchemes
{
    class AreaTypeMappingSchemeRegistrator : IAreaTypeMappingSchemeRegistrator
    {
        public void Register()
        {
            AggregateConfiguration.Aggregates
               .Register<AreaType>("AreaType", null);

            AggregateConfiguration.Aggregates
                .Register<AreaType>("FullGraph", configuration => configuration
                                    .AssociatedCollection(areaType => areaType.Areas));
        }
    }
}
