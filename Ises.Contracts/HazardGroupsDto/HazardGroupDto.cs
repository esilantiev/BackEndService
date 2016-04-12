using Ises.Contracts.HazardsDto;
using System.Collections.Generic;

namespace Ises.Contracts.HazardGroupsDto
{
    public class HazardGroupDto : BaseDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<HazardDto> Hazards { get; set; }  
    
    }
}
