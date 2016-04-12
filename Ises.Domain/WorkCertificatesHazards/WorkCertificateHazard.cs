using Ises.Domain.Hazards;
using Ises.Domain.WorkCertificates;

namespace Ises.Domain.WorkCertificatesHazards
{
    public class WorkCertificateHazard
    {
        public long WorkCertificateId { get; set; }
        public long HazardId { get; set; }

        public int? InitialRisk { get; set; }
        public int? ResidualRisk { get; set; }
        public string InitialRiskName { get; set; }
        public string ResidualRiskName { get; set; }
        public bool? Alarp { get; set; }
        public bool? IsAutomaticAlarp { get; set; }

        public virtual WorkCertificate WorkCertificate { get; set; }
        public virtual Hazard Hazard { get; set; }
    }
}
