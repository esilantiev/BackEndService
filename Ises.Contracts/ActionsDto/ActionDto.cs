using Ises.Contracts.CertificatesUsersDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ises.Contracts.ActionsDto
{
    public class ActionDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CertificateUserDto> CertificateUsers { get; set; }

    }
}
