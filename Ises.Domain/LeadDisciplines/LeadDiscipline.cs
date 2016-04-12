using System.Collections.Generic;
using Ises.Domain.BaseCertificates;
using Ises.Domain.IsolationCertificates;
using Ises.Domain.Organizations;
using Ises.Domain.Positions;
using Ises.Domain.WorkCertificates;

namespace Ises.Domain.LeadDisciplines
{
    public class LeadDiscipline
    {
        public long Id { get; set; }
        public long OrganizationId { get; set; }

        public string Name { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual ICollection<BaseCertificate> BaseCertificates { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
    }
}
