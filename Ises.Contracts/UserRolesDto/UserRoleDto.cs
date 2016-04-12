using Ises.Contracts.CertificatesUsersDto;
using Ises.Contracts.HistoryChangesDto;
using Ises.Contracts.RolePermissionsDto;
using Ises.Contracts.UsersDto;
using System.Collections.Generic;

namespace Ises.Contracts.UserRolesDto
{
    public class UserRoleDto : BaseDto
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string ShortEnglishName { get; set; }

        public virtual ICollection<UserDto> Users { get; set; }
        public virtual ICollection<RolePermissionDto> RolePermissions { get; set; }
        public virtual ICollection<HistoryChangeDto> UserRoleHistoryChanges { get; set; }
        public virtual ICollection<CertificateUserDto> CertificateUsers { get; set; }
      
    }
}