using System.Collections.Generic;
using Ises.Domain.Locations;
using Ises.Domain.Users;

namespace Ises.Domain.Installations
{
    public class Installation
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
