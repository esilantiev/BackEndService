using Ises.Contracts.AreasDto;
using Ises.Contracts.InstallationsDto;
using System.Collections.Generic;

namespace Ises.Contracts.LocationsDto 
{
    public class LocationDto : BaseDto
    {
        public long Id { get; set; }
        public long InstallationId { get; set; }

        public string Name { get; set; }

        public virtual InstallationDto Installation { get; set; }
        public virtual ICollection<AreaDto> Areas { get; set; }
    
    }
}
