using System.Data.Entity.ModelConfiguration;
using Ises.Domain.WorkCertificatesWorkCertificates;

namespace Ises.Data.EntityTypeConfigurations
{
    public class WorkCertificateWorkCertificateTypeConfiguration : EntityTypeConfiguration<WorkCertificateWorkCertificate>
    {
        public WorkCertificateWorkCertificateTypeConfiguration()
        {
            HasKey(workCertificateWorkCertificate => new { workCertificateWorkCertificate.FirstWorkCertificateId, 
                                                           workCertificateWorkCertificate.SecondWorkCertificateId, 
                                                           workCertificateWorkCertificate.ConnectionType });
  
            HasRequired(workCertificateWorkCertificate => workCertificateWorkCertificate.FirstWorkCertificate)
                .WithMany(w => w.WorkCertificatesWorkCertificatesAsSource)
                .HasForeignKey(ww => ww.FirstWorkCertificateId)
                .WillCascadeOnDelete(false);

            HasRequired(ww => ww.SecondWorkCertificate)
                .WithMany(w => w.WorkCertificatesWorkCertificatesAsTarget)
                .HasForeignKey(ww => ww.SecondWorkCertificateId)
                .WillCascadeOnDelete(false);

            ToTable("WorkCertificatesWorkCertificates");
        }
    }
}
