using System.Data.Entity.ModelConfiguration;
using Ises.Domain.WorkCertificatesHazards;

namespace Ises.Data.EntityTypeConfigurations
{
    public class WorkCertificateHazardTypeConfiguration : EntityTypeConfiguration<WorkCertificateHazard>
    {
        public WorkCertificateHazardTypeConfiguration()
        {
            HasKey(workCertificateHazard => new { 
                                                    workCertificateHazard.WorkCertificateId, 
                                                    workCertificateHazard.HazardId 
                                                });
            
            HasRequired(workCertificateHazard => workCertificateHazard.WorkCertificate)
                .WithMany(workCertificate => workCertificate.Hazards)
                .HasForeignKey(workCertificateHazard => workCertificateHazard.WorkCertificateId)
                .WillCascadeOnDelete(false);

            HasRequired(workCertificateHazard => workCertificateHazard.Hazard)
                .WithMany(hazard => hazard.WorkCertificateHazards)
                .HasForeignKey(workCertificateHazard => workCertificateHazard.HazardId)
                .WillCascadeOnDelete(false);

            ToTable("WorkCertificatesHazards");
        }
    }
}
