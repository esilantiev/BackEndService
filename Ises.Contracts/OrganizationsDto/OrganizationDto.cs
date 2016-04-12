using Ises.Contracts.LeadDisciplinesDto;
using System.Collections.Generic;

namespace Ises.Contracts.OrganizationsDto
{
    public class OrganizationDto : BaseDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<LeadDisciplineDto> LeadDisciplines { get; set; }
    
    }
}
