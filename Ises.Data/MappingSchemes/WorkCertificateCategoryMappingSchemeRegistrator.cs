using Ises.Domain.WorkCertificateCategories;
using RefactorThis.GraphDiff.Aggregates;

namespace Ises.Data.MappingSchemes
{
    public class WorkCertificateCategoryMappingSchemeRegistrator :IWorkCertificateCategoryMappingSchemeRegistrator
    {
        public void Register()
        {
            AggregateConfiguration.Aggregates
                .Register<WorkCertificateCategory>("WorkCertificateCategory", null);
        }
    }
}
