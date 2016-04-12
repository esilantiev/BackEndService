using Ises.Contracts.Common;
using Ises.Contracts.WorkCertificatesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ises.Contracts.WorkCertificatesWorkCertificatesDto
{
    public class WorkCertificateWorkCertificateDto
    {
        public long FirstWorkCertificateId { get; set; }
        public long SecondWorkCertificateId { get; set; }

        public CrossReferenceConnectionType ConnectionType { get; set; }

        public virtual WorkCertificateDto FirstWorkCertificate { get; set; }
        public virtual WorkCertificateDto SecondWorkCertificate { get; set; }
    
    }
}
