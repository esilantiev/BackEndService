using Ises.Domain.Installations;
using RefactorThis.GraphDiff;
using RefactorThis.GraphDiff.Aggregates;

namespace Ises.Data.MappingSchemes
{
    public class InstallationMappingSchemeRegistrator : IInstallationMappingSchemeRegistrator
    {
        public void Register()
        {
            AggregateConfiguration.Aggregates
                .Register<Installation>("Installation", null);

            AggregateConfiguration.Aggregates
                .Register<Installation>("FullGraph", configuration => configuration
                                    .OwnedCollection(installation => installation.Locations));
        }
    }
}
