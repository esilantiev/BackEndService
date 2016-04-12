using Ises.Contracts.BaseCertificatesDto;
using Ises.Contracts.Common;
using Ises.Contracts.IsolationPointsDto;
using Ises.Contracts.WorkCertificatesDto;
using System;
using System.Collections.Generic;

namespace Ises.Contracts.IsolationCertificatesDto
{
    public class IsolationCertificateDto : BaseCertificateDto
    {
        public IsolationCertificateDto()
        {
            CertificateType = BaseCertificateType.IsolationCertificate;
        }
        public IsolationCertificateStatus Status { get; set; }
        public IsolationCertificateType Type { get; set; }
        public DateTime IsolationDate { get; set; }
        public string ContingencyPlan { get; set; }
        public string MaintenanceNumber { get; set; }
        
        public virtual ICollection<WorkCertificateDto> WorkCertificates { get; set; }
        public virtual ICollection<IsolationPointDto> IsolationPoints { get; set; }
        
    }
}
