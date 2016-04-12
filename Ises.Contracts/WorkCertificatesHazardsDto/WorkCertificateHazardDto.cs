using Ises.Contracts.HazardsDto;
using Ises.Contracts.WorkCertificatesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ises.Contracts.WorkCertificatesHazardsDto
{
    public class WorkCertificateHazardDto
    {
        public long WorkCertificateId { get; set; }
        public long HazardId { get; set; }

        public int? InitialRisk { get; set; }
        public int? ResidualRisk { get; set; }
        public string InitialRiskName { get; set; }
        public string ResidualRiskName { get; set; }
        public bool? Alarp { get; set; }
        public bool? IsAutomaticAlarp { get; set; }

        public virtual WorkCertificateDto WorkCertificate { get; set; }
        public virtual HazardDto Hazard { get; set; }
    }
}
