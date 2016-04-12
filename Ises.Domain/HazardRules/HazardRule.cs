using System.Collections.Generic;
using Ises.Domain.AreaTypes;
using Ises.Domain.HazardControls;
using Ises.Domain.WorkCertificateCategories;

namespace Ises.Domain.HazardRules
{
    public class HazardRule
    {
        public long Id { get; set; }
        public long WorkCertificateCategoryId { get; set; }
        public long AreaTypeId { get; set; }

        public WorkCertificateCategory WorkCertificateCategory { get; set; }
        public AreaType AreaType { get; set; }
        public virtual ICollection<HazardControl> HazardControls { get; set; }
    }
}
