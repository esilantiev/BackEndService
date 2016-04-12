using System.Collections.Generic;
using Ises.Domain.LeadDisciplines;
using Ises.Domain.Users;

namespace Ises.Domain.Positions
{
    public class Position
    {
        public long Id { get; set; }
        public long LeadDisciplineId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual LeadDiscipline LeadDiscipline { get; set; }
    }
}
