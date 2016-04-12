using Ises.Contracts.Common;
using Ises.Contracts.UserRolesDto;
using Ises.Contracts.UsersDto;
using System;

namespace Ises.Contracts.HistoryChangesDto
{
    public class HistoryChangeDto : BaseDto
    {
        public HistoryChangeDto()
        {
            ModificationDate = DateTime.UtcNow;
        }
        public long Id { get; set; }
        public long UserId { get; set; }
        public long UserRoleId { get; set; }
        public long EntityId { get; set; }

        public EntityType EntityType { get; set; }
        private DateTime ModificationDate { get; set; }

        public virtual UserRoleDto UserRole { get; set; }
        public virtual UserDto User { get; set; }
    }
}
