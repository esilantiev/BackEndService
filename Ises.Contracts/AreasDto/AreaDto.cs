using Ises.Contracts.AreaTypesDto;
using Ises.Contracts.LocationsDto;
using Ises.Contracts.UsersDto;
using Ises.Contracts.WorkCertificatesAreasDto;
using System.Collections.Generic;

namespace Ises.Contracts.AreasDto
{
    public class AreaDto : BaseDto
    {
        public long Id { get; set; }
        public long LocationId { get; set; }
        public long AreaTypeId { get; set; }

        public string Name { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual AreaTypeDto AreaType { get; set; }
        public virtual LocationDto Location { get; set; }
        public virtual ICollection<WorkCertificateAreaDto> WorkCertificateAreas { get; set; }
        public virtual ICollection<UserDto> Users { get; set; }
    }
}
