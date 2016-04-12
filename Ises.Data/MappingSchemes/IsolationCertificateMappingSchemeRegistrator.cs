using Ises.Domain.IsolationCertificates;
using RefactorThis.GraphDiff;
using RefactorThis.GraphDiff.Aggregates;

namespace Ises.Data.MappingSchemes
{
    public class IsolationCertificateMappingSchemeRegistrator :IIsolationCertificateMappingSchemeRegistrator
    {
        public void Register()
        {
            AggregateConfiguration.Aggregates
                .Register<IsolationCertificate>("IsolationCertificate", null);

            AggregateConfiguration.Aggregates
                .Register<IsolationCertificate>("FullGraph", configuration => configuration
                                    .OwnedCollection(isolationCertificate => isolationCertificate.ExternalDocuments));
        }
    }
}
