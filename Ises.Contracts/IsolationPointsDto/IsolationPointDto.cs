using Ises.Contracts.IsolationCertificatesDto;

namespace Ises.Contracts.IsolationPointsDto
{
    public class IsolationPointDto : BaseDto
    {
        public long Id { get; set; }
        public long IsolationCertificateId { get; set; }

        public string TagNumber { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Method { get; set; }
        public string Comment { get; set; }
        public string LoLc { get; set; }
        public string IsolatedState { get; set; }
        public string DeIsolatedState { get; set; }

        public virtual IsolationCertificateDto IsolationCertificate { get; set; }
    
    }
}
