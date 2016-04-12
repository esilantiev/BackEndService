using Ises.Contracts.ActionsDto;
using Ises.Contracts.BaseCertificatesDto;
using Ises.Contracts.Common;
using Ises.Contracts.UsersDto;

namespace Ises.Contracts.CertificatesUsersDto
{
    public class CertificateUserDto : BaseDto
    {
        public long Id { get; set; }
        public long BaseCertificateId { get; set; }
        public long UserId { get; set; }
        public long ActionId { get; set; }

        public bool IsMandatoryAction { get; set; }
        public ActionStatus ActionStatus { get; set; }

        public virtual UserDto User { get; set; }
        public virtual ActionDto Action { get; set; }
        public virtual BaseCertificateDto Certificate { get; set; }
    }
}
