using Ises.Domain.BaseCertificates;

namespace Ises.Domain.ExternalDocuments
{
    public class ExternalDocument
    {
        public long Id { get; set; }
        public long BaseCertificateId { get; set; }

        public string DocumentName { get; set; }
        public string Url { get; set; }

        public virtual BaseCertificate BaseCertificate { get; set; }
    }
}
