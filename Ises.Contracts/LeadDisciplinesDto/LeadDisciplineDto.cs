using Ises.Contracts.IsolationCertificatesDto;
using Ises.Contracts.OrganizationsDto;
using Ises.Contracts.PositionsDto;
using Ises.Contracts.WorkCertificatesDto;
using System.Collections.Generic;

namespace Ises.Contracts.LeadDisciplinesDto
{
    public class LeadDisciplineDto : BaseDto
    {
        public long Id { get; set; }
        public long OrganizationId { get; set; }

        public string Name { get; set; }

        public virtual OrganizationDto Organization { get; set; }
        public virtual ICollection<WorkCertificateDto> WorkCertificates { get; set; }
        public virtual ICollection<IsolationCertificateDto> IsolationCertificates { get; set; }
        public virtual ICollection<PositionDto> Positions { get; set; }
    
    }
}
