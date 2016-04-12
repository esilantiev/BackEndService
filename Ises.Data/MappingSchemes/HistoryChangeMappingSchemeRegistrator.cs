using Ises.Domain.HistoryChanges;
using RefactorThis.GraphDiff.Aggregates;

namespace Ises.Data.MappingSchemes
{
    public class HistoryChangeMappingSchemeRegistrator : IHistoryChangeMappingSchemeRegistrator
    {
        public void Register()
        {
            AggregateConfiguration.Aggregates
                .Register<HistoryChange>("HistoryChange", null);
        }
    }
}
