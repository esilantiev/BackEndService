using Ises.Domain.Positions;
using RefactorThis.GraphDiff.Aggregates;

namespace Ises.Data.MappingSchemes
{
    public class PositionMappingSchemeRegistrator : IPositionMappingSchemeRegistrator
    {
        public void Register()
        {
            AggregateConfiguration.Aggregates
                .Register<Position>("FullGraph", null);
        }
    }
}
