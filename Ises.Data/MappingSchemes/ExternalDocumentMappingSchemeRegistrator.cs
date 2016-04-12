using Ises.Domain.ExternalDocuments;
using RefactorThis.GraphDiff.Aggregates;

namespace Ises.Data.MappingSchemes
{
    public class ExternalDocumentMappingSchemeRegistrator : IExternalDocumentMappingSchemeRegistrator
    {
        public void Register()
        {
            AggregateConfiguration.Aggregates
                .Register<ExternalDocument>("ExternalDocument", null);
        }
    }
}
