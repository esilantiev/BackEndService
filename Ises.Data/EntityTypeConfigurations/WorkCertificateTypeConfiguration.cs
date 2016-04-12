using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Ises.Domain.WorkCertificates;

namespace Ises.Data.EntityTypeConfigurations
{
    public class WorkCertificateTypeConfiguration : EntityTypeConfiguration<WorkCertificate>
    {
        public WorkCertificateTypeConfiguration()
        {
            HasKey(workCertificate => workCertificate.Id);

            Property(workCertificate => workCertificate.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(workCertificate => workCertificate.Type).IsRequired();
            Property(workCertificate => workCertificate.Title).IsRequired();
            Property(workCertificate => workCertificate.Description).IsRequired();

            HasMany(workCertificate => workCertificate.WorkCertificateAreas)
               .WithRequired()
               .HasForeignKey(cp => cp.WorkCertificateId);

            HasMany(workCertificate => workCertificate.IsolationCertificates).
                WithMany(isolationCertificate => isolationCertificate.WorkCertificates).
                Map(m =>
                    {
                        m.MapLeftKey("WorkCertificateId");
                        m.MapRightKey("IsolationCertificateId");
                        m.ToTable("WorkCertificatesIsolationCertificates");
                    });

            ToTable("WorkCertificate"); 
        }
    }
}
