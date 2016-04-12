using Ises.Domain.WorkCertificates;
using RefactorThis.GraphDiff;
using RefactorThis.GraphDiff.Aggregates;

namespace Ises.Data.MappingSchemes
{
    public class WorkCertificateMappingSchemeRegistrator :IWorkCertificateMappingSchemeRegistrator
    {
        public void Register()
        {
            AggregateConfiguration.Aggregates
               .Register<WorkCertificate>("WorkCertificate", null);

            AggregateConfiguration.Aggregates
                .Register<WorkCertificate>("FullGraph", configuration => configuration
                                    .AssociatedCollection(workCertificate => workCertificate.WorkCertificateAreas)
                                    .OwnedCollection(workCertificate=>workCertificate.ExternalDocuments)
                                    .OwnedCollection(workCertificate => workCertificate.HazardControls)
                                    .OwnedCollection(workCertificate => workCertificate.Hazards)
                                    .AssociatedCollection(workCertificate => workCertificate.IsolationCertificates)
                                    .AssociatedCollection(workCertificate => workCertificate.WorkCertificatesWorkCertificatesAsSource)
                                    .AssociatedCollection(workCertificate => workCertificate.WorkCertificateAreas));
        }
    }
}
