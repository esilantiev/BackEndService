using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ises.Contracts.Common;
using Ises.Domain.CertificatesUsers;
using Ises.Domain.ExternalDocuments;
using Ises.Domain.LeadDisciplines;
using Ises.Domain.Users;

namespace Ises.Domain.BaseCertificates
{
    public class BaseCertificate
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long LeadDisciplineId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string TagNumber { get; set; }
        protected BaseCertificateType CertificateType { get; set; }
        public YesNoOption IsTranslationRequired { get; set; }

        public virtual User User { get; set; }
        public virtual LeadDiscipline LeadDiscipline { get; set; }
        public virtual ICollection<ExternalDocument> ExternalDocuments { get; set; }
        public virtual ICollection<CertificateUser> CertificateUsers { get; set; }
    }
}
