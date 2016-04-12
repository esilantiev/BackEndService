using System.Collections.Generic;
using Ises.Domain.LeadDisciplines;

namespace Ises.Domain.Organizations
{
    public class Organization
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<LeadDiscipline> LeadDisciplines { get; set; }
    }
}
