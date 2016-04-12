using Ises.Contracts.AreasDto;
using System.Collections.Generic;

namespace Ises.Contracts.AreaTypesDto
{
    public class AreaTypeDto : BaseDto
    {
        public long Id { get; set; }

        public string  Name { get; set; }
        
        public virtual ICollection<AreaDto> Areas { get; set; }
    }
}
