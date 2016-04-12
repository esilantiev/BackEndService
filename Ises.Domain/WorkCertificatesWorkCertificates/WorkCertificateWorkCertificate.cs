using Ises.Contracts.Common;
using Ises.Domain.WorkCertificates;

namespace Ises.Domain.WorkCertificatesWorkCertificates
{
    public class WorkCertificateWorkCertificate
    {
        public long FirstWorkCertificateId { get; set; }
        public long SecondWorkCertificateId { get; set; }
        
        public CrossReferenceConnectionType ConnectionType { get; set; }

        public virtual WorkCertificate FirstWorkCertificate { get; set; }
        public virtual WorkCertificate SecondWorkCertificate { get; set; }
    }
}
