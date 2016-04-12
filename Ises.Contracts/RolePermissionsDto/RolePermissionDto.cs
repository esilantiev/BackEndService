using Ises.Contracts.UserRolesDto;
using System.Collections.Generic;

namespace Ises.Contracts.RolePermissionsDto
{
    public class RolePermissionDto : BaseDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserRoleDto> UserRoles { get; set; }
    
    }
}
