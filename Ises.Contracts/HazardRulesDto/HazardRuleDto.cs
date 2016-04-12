using Ises.Contracts.AreaTypesDto;
using Ises.Contracts.HazardControlsDto;
using Ises.Contracts.WorkCertificateCategoriesDto;
using System.Collections.Generic;

namespace Ises.Contracts.HazardRulesDto
{
    public class HazardRuleDto : BaseDto
    {
        public long Id { get; set; }
        public long WorkCertificateCategoryId { get; set; }
        public long AreaTypeId { get; set; }

        public WorkCertificateCategoryDto WorkCertificateCategory { get; set; }
        public AreaTypeDto AreaType { get; set; }
        public virtual ICollection<HazardControlDto> HazardControls { get; set; }
    
    }
}
