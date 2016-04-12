using Ises.Contracts.Common;
using Ises.Contracts.HazardControlsDto;
using Ises.Contracts.WorkCertificatesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ises.Contracts.WorkCertificatesHazardControlsDto
{
    public class WorkCertificateHazardControlDto
    {
        public long WorkCertificateId { get; set; }
        public long HazardControlId { get; set; }

        public HazardControlType HazardControlType { get; set; }
        public bool IsAutomaticallyCalculated { get; set; }

        public virtual WorkCertificateDto WorkCertificate { get; set; }
        public virtual HazardControlDto HazardControl { get; set; }
    
    }
}
