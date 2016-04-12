using System.Collections.Generic;
using Ises.Domain.WorkCertificates;

namespace Ises.Domain.WorkCertificateCategories
{
    public class WorkCertificateCategory
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<WorkCertificate> WorkCertificates { get; set; }
    }
}
