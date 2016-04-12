using Ises.Contracts.CertificatesUsersDto;
using Ises.Contracts.Common;
using Ises.Contracts.ExternalDocumentsDto;
using Ises.Contracts.LeadDisciplinesDto;
using Ises.Contracts.UsersDto;
using System.Collections.Generic;

namespace Ises.Contracts.BaseCertificatesDto
{
    public class BaseCertificateDto : BaseDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long LeadDisciplineId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string TagNumber { get; set; }
        public BaseCertificateType CertificateType { get; set; }
        public YesNoOption IsTranslationRequired { get; set; }

        public virtual UserDto User { get; set; }
        public virtual LeadDisciplineDto LeadDiscipline { get; set; }
        public virtual ICollection<ExternalDocumentDto> ExternalDocuments { get; set; }
        public virtual ICollection<CertificateUserDto> CertificateUsers { get; set; }
    }
}
