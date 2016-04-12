namespace Ises.Contracts.ExternalDocumentsDto
{
    public class ExternalDocumentDto : BaseDto
    {
        public long Id { get; set; }
        public long CertificateId { get; set; }

        public string DocumentName { get; set; }
        public string Url { get; set; }
    }
}
