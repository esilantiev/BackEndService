using System.Collections.Generic;
using Ises.Domain.UserRoles;

namespace Ises.Domain.RolePermissions
{
    public class RolePermission
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
