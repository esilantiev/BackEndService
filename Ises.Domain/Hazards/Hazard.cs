using System.Collections.Generic;
using Ises.Domain.HazardControls;
using Ises.Domain.HazardGroups;
using Ises.Domain.WorkCertificatesHazards;

namespace Ises.Domain.Hazards
{
    public class Hazard
    {
        public long Id { get; set; }
        public long HazardGroupId { get; set; }

        public string Name { get; set; }

        public virtual HazardGroup HazardGroup { get; set; }
        public virtual ICollection<HazardControl> HazardControls { get; set; }
        public virtual ICollection<WorkCertificateHazard> WorkCertificateHazards { get; set; }
    }
}
