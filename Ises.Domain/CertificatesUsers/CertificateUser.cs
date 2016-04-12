using Ises.Contracts.Common;
using Ises.Domain.Actions;
using Ises.Domain.BaseCertificates;
using Ises.Domain.Users;

namespace Ises.Domain.CertificatesUsers
{
    public class CertificateUser
    {
       public long Id { get; set; }
       public long BaseCertificateId { get; set; }
       public long UserId { get; set; }
       public long ActionId { get; set; }

       public bool IsMandatoryAction { get; set; }
       public ActionStatus ActionStatus { get; set; }

       public virtual User User { get; set; }
       public virtual Action Action { get; set; }
       public virtual BaseCertificate Certificate { get; set; }
    }
}
