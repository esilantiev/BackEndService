using Ises.Contracts.HazardControlsDto;
using Ises.Contracts.HazardGroupsDto;
using Ises.Contracts.WorkCertificatesHazardsDto;
using System.Collections.Generic;

namespace Ises.Contracts.HazardsDto
{
    public class HazardDto : BaseDto
    {
        public long Id { get; set; }
        public long HazardGroupId { get; set; }

        public string Name { get; set; }

        public virtual HazardGroupDto HazardGroup { get; set; }
        public virtual ICollection<HazardControlDto> HazardControls { get; set; }
        public virtual ICollection<WorkCertificateHazardDto> WorkCertificateHazards { get; set; }
    

    }
}
