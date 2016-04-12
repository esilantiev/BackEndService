using System.Collections.Generic;
using Ises.Domain.HazardRules;
using Ises.Domain.Hazards;
using Ises.Domain.WorkCertificatesHazardControls;

namespace Ises.Domain.HazardControls
{
    public class HazardControl
    {
        public long Id { get; set; }
        public long HazardId { get; set; }

        public string Name { get; set; }

        public virtual Hazard Hazard { get; set; }
        public virtual ICollection<HazardRule> HazardRules { get; set; }
        public virtual ICollection<WorkCertificateHazardControl> WorkCertificatesHazardControls { get; set; }
    }
}
