using System.Data.Entity.ModelConfiguration;
using Ises.Domain.WorkCertificateCategories;

namespace Ises.Data.EntityTypeConfigurations
{
    public class WorkCertificateCategoryTypeConfiguration : EntityTypeConfiguration<WorkCertificateCategory>
    {
        public WorkCertificateCategoryTypeConfiguration()
        {
            ToTable("WorkCertificateCategory");
        }
    }
}
