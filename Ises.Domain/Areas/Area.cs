using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ises.Domain.AreaTypes;
using Ises.Domain.Locations;
using Ises.Domain.Users;
using Ises.Domain.WorkCertificatesAreas;

namespace Ises.Domain.Areas
{
   
    public class Area
    {
        public long Id { get; set; }
        public long LocationId { get; set; }
        public long AreaTypeId { get; set; }

        public string Name { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual AreaType AreaType { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<WorkCertificateArea> WorkCertificateAreas { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }



}
