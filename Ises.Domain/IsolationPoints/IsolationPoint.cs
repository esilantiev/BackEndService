using Ises.Contracts.Common;
using Ises.Domain.IsolationCertificates;
using Ises.Domain.IsolationTypes;

namespace Ises.Domain.IsolationPoints
{
    public class IsolationPoint
    {
        public long Id { get; set; }
        public long IsolationCertificateId { get; set; }
        public long IsolationTypeId { get; set; }

        public string TagNumber { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public IsolationPointLoLc LoLc { get; set; }

        public virtual IsolationCertificate IsolationCertificate { get; set; }
        public virtual IsolationType IsolationType { get; set; }
    }
}
