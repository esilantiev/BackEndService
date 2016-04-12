using System.Data.Entity.ModelConfiguration;
using Ises.Domain.WorkCertificatesAreas;

namespace Ises.Data.EntityTypeConfigurations
{
    public class WorkCertificateAreaTypeConfiguration : EntityTypeConfiguration<WorkCertificateArea>
    {
        public WorkCertificateAreaTypeConfiguration()
        {
            HasKey(workCertificateArea => new { workCertificateArea.WorkCertificateId, workCertificateArea.AreaId });
        }
    }
}
