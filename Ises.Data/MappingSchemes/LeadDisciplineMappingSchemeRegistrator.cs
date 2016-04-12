using Ises.Domain.LeadDisciplines;
using RefactorThis.GraphDiff;
using RefactorThis.GraphDiff.Aggregates;

namespace Ises.Data.MappingSchemes
{
    public class LeadDisciplineMappingSchemeRegistrator : ILeadDisciplineMappingSchemeRegistrator
    {
        public void Register()
        {
            AggregateConfiguration.Aggregates
                .Register<LeadDiscipline>("LeadDiscipline", null);

            AggregateConfiguration.Aggregates
                .Register<LeadDiscipline>("FullGraph", configuration => configuration
                                    .AssociatedCollection(leadDiscipline => leadDiscipline.Positions));
        }
    }
}
