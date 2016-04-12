using Ises.Contracts.AreasDto;
using Ises.Contracts.CertificatesUsersDto;
using Ises.Contracts.Common;
using Ises.Contracts.HistoryChangesDto;
using Ises.Contracts.InstallationsDto;
using Ises.Contracts.IsolationCertificatesDto;
using Ises.Contracts.PositionsDto;
using Ises.Contracts.UserRolesDto;
using Ises.Contracts.WorkCertificatesDto;
using System.Collections.Generic;

namespace Ises.Contracts.UsersDto
{
    public class UserDto : BaseDto
    {
        public UserDto()
        {
            ContactDetails = new ContactDetailsDto();
        }
        public long Id { get; set; }
        public long? ManagerId { get; set; }
        public long InstallationId { get; set; }
        public long PositionId { get; set; }

        public string Name { get; set; }
        public string Login { get; set; }
        public byte[] PersonImage { get; set; }
        public YesNoOption IsAtWork { get; set; }
        public UserShift Shift { get; set; }

        public ContactDetailsDto ContactDetails { get; set; }
        public virtual PositionDto Position{ get; set; }
        public virtual InstallationDto Installation { get; set; }
        public virtual UserDto Manager { get; set; }
        public virtual ICollection<UserRoleDto> UserRoles { get; set; }
        public virtual ICollection<AreaDto> Areas { get; set; }
        public virtual ICollection<CertificateUserDto> CertificateUsers { get; set; }
        public virtual ICollection<WorkCertificateDto> WorkCertificates { get; set; }
        public virtual ICollection<IsolationCertificateDto> IsolationCertificates { get; set; }
        public virtual ICollection<UserDto> Favorites { get; set; }
        public virtual ICollection<HistoryChangeDto> UserHistoryChanges { get; set; }
    }
}
