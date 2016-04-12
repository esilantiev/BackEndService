using System;
using Ises.Contracts.Common;
using Ises.Domain.UserRoles;
using Ises.Domain.Users;

namespace Ises.Domain.HistoryChanges
{
    public class HistoryChange
    {
        public HistoryChange()
        {
            ModificationDate = DateTime.UtcNow;
        }
        public long Id { get; set; }
        public long UserId { get; set; }
        public long UserRoleId { get; set; }
        public long EntityId { get; set; }

        public EntityType EntityType { get; set; }
        private DateTime ModificationDate { get; set; }

        public virtual UserRole UserRole { get; set; }
        public virtual User User { get; set; }
    }
}
