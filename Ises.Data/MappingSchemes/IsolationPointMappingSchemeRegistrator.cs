using Ises.Domain.IsolationPoints;
using RefactorThis.GraphDiff;
using RefactorThis.GraphDiff.Aggregates;

namespace Ises.Data.MappingSchemes
{
    public class IsolationPointMappingSchemeRegistrator : IIsolationPointMappingSchemeRegistrator
    {
        public void Register()
        {
            AggregateConfiguration.Aggregates
                .Register<IsolationPoint>("IsolationPoint", null);

            AggregateConfiguration.Aggregates
                .Register<IsolationPoint>("FullGraph", configuration => configuration
                                    .OwnedEntity(isolationPoint => isolationPoint.IsolationCertificate));
        }
    }
}
