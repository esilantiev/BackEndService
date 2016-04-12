using Ises.Contracts.WorkCertificatesDto;
using System.Collections.Generic;

namespace Ises.Contracts.WorkCertificateCategoriesDto
{
    public class WorkCertificateCategoryDto : BaseDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<WorkCertificateDto> WorkCertificates { get; set; }
    }
}
