using System.Collections.Generic;
using Ises.Domain.Hazards;

namespace Ises.Domain.HazardGroups
{
    public class HazardGroup
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Hazard> Hazards { get; set; }  
    }
}
