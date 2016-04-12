using Ises.Contracts.LeadDisciplinesDto;
using Ises.Contracts.UsersDto;
using System.Collections.Generic;

namespace Ises.Contracts.PositionsDto
{
    public class PositionDto : BaseDto
    {
        public long Id { get; set; }
        public long LeadDisciplineId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserDto> Users { get; set; }
        public virtual LeadDisciplineDto LeadDiscipline { get; set; }
    
    }
}
