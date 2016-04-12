using System;
using System.Collections.Generic;
using Ises.Contracts.Common;
using Ises.Domain.BaseCertificates;
using Ises.Domain.IsolationPoints;
using Ises.Domain.WorkCertificates;

namespace Ises.Domain.IsolationCertificates
{
    public class IsolationCertificate : BaseCertificate
    {
        public IsolationCertificate()
        {
            CertificateType = BaseCertificateType.IsolationCertificate;
        }

        public IsolationCertificateStatus Status { get; set; }
        public IsolationCertificateType Type { get; set; }
        public DateTime IsolationDate { get; set; }
        public string ContingencyPlan { get; set; }
        public string MaintenanceNumber { get; set; }
        
        public virtual ICollection<WorkCertificate> WorkCertificates { get; set; }
        public virtual ICollection<IsolationPoint> IsolationPoints { get; set; }
        
    }
}
