using Ises.Contracts.LocationsDto;
using Ises.Contracts.UsersDto;
using System.Collections.Generic;

namespace Ises.Contracts.InstallationsDto
{
    public class InstallationDto : BaseDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<LocationDto> Locations { get; set; }
        public virtual ICollection<UserDto> Users { get; set; }
    
    }
}
