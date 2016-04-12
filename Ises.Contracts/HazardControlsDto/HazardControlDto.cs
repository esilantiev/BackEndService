using Ises.Contracts.HazardRulesDto;
using Ises.Contracts.HazardsDto;
using Ises.Contracts.WorkCertificatesHazardControlsDto;
using System.Collections.Generic;

namespace Ises.Contracts.HazardControlsDto
{
    public class HazardControlDto : BaseDto
    {
        public long Id { get; set; }
        public long HazardId { get; set; }

        public string Name { get; set; }

        public virtual HazardDto Hazard { get; set; }
        public virtual ICollection<HazardRuleDto> HazardRules { get; set; }
        public virtual ICollection<WorkCertificateHazardControlDto> WorkCertificatesHazardControls { get; set; }
    }
}
