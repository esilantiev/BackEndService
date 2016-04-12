using Ises.Domain.Areas;
using Ises.Domain.WorkCertificates;

namespace Ises.Domain.WorkCertificatesAreas
{
    public class WorkCertificateArea
    {
        public long WorkCertificateId { get; set; }
        public long AreaId { get; set; }

        public bool IsMainArea { get; set; }

        public virtual WorkCertificate WorkCertificate { get; set; }
        public virtual Area Area { get; set; }
    }
}
