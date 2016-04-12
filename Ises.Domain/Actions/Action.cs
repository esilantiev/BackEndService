using System.Collections.Generic;
using Ises.Domain.CertificatesUsers;

namespace Ises.Domain.Actions
{
    public class Action
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CertificateUser> CertificateUsers { get; set; }
    }
}
