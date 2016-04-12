using Ises.Domain.CertificatesUsers;
using RefactorThis.GraphDiff;
using RefactorThis.GraphDiff.Aggregates;

namespace Ises.Data.MappingSchemes
{
    public class CertificateUserMappingSchemeRegistrator : ICertificateUserMappingSchemeRegistrator
    {
        public void Register()
        {
            AggregateConfiguration.Aggregates
                .Register<CertificateUser>("CertificateUser", null);

            AggregateConfiguration.Aggregates
                .Register<CertificateUser>("FullGraph", configuration => configuration
                                    .AssociatedEntity(certificateUser => certificateUser.Action));
        }
    }
}
