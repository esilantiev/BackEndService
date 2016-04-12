using System.Collections.Generic;
using Ises.Domain.Areas;
using Ises.Domain.Installations;

namespace Ises.Domain.Locations
{
    public class Location
    {
        public long Id { get; set; }
        public long InstallationId { get; set; }

        public string Name { get; set; }

        public virtual Installation Installation { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
    }
}
