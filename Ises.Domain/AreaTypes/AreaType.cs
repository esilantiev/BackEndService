using System.Collections.Generic;
using Ises.Domain.Areas;

namespace Ises.Domain.AreaTypes
{
    public class AreaType
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Area> Areas { get; set; }
    }
}
