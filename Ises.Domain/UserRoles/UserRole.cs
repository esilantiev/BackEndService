using System.Collections.Generic;
using Ises.Domain.HistoryChanges;
using Ises.Domain.RolePermissions;
using Ises.Domain.Users;

namespace Ises.Domain.UserRoles
{
    public class UserRole
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string ShortEnglishName { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<HistoryChange> UserRoleHistoryChanges { get; set; }
    }
}