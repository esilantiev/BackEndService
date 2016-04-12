using System.Collections.Generic;
using Ises.Contracts.Common;
using Ises.Domain.Areas;
using Ises.Domain.BaseCertificates;
using Ises.Domain.CertificatesUsers;
using Ises.Domain.HistoryChanges;
using Ises.Domain.Installations;
using Ises.Domain.Positions;
using Ises.Domain.UserRoles;

namespace Ises.Domain.Users
{
    public class User
    {
        public User()
        {
            ContactDetails = new ContactDetails();
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

        public ContactDetails ContactDetails { get; set; }
        public virtual Position Position { get; set; }
        public virtual Installation Installation { get; set; }
        public virtual User Manager { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<CertificateUser> CertificateUsers { get; set; }
        public virtual ICollection<BaseCertificate> BaseCertificates { get; set; }
        public virtual ICollection<User> Favorites { get; set; }
        public virtual ICollection<HistoryChange> UserHistoryChanges { get; set; }
    }
}
