using Ises.Contracts.Common;
using Ises.Domain.HazardControls;
using Ises.Domain.WorkCertificates;

namespace Ises.Domain.WorkCertificatesHazardControls
{
    public class WorkCertificateHazardControl
    {
        public long WorkCertificateId { get; set; }
        public long HazardControlId { get; set; }

        public HazardControlType HazardControlType { get; set; }
        public bool IsAutomaticallyCalculated { get; set; }  

        public virtual WorkCertificate WorkCertificate { get; set; }
        public virtual HazardControl HazardControl { get; set; }
    }
}
