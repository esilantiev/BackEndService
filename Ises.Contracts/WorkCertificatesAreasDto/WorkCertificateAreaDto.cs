using Ises.Contracts.AreasDto;
using Ises.Contracts.WorkCertificatesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ises.Contracts.WorkCertificatesAreasDto
{
    public class WorkCertificateAreaDto
    {
        public long WorkCertificateId { get; set; }
        public long AreaId { get; set; }

        public bool IsMainArea { get; set; }

        public virtual WorkCertificateDto WorkCertificate { get; set; }
        public virtual AreaDto Area { get; set; }
    }
}
