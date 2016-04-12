using System.Data.Entity.ModelConfiguration;
using Ises.Domain.WorkCertificatesHazardControls;

namespace Ises.Data.EntityTypeConfigurations
{
    public class WorkCertificateHazardControlTypeConfiguration : EntityTypeConfiguration<WorkCertificateHazardControl>
    {
        public WorkCertificateHazardControlTypeConfiguration()
        {
            HasKey(workCertificateHazardControl => new  {
                                                            workCertificateHazardControl.WorkCertificateId,
                                                            workCertificateHazardControl.HazardControlId
                                                        });

            HasRequired(workCertificateHazardControl => workCertificateHazardControl.WorkCertificate)
                .WithMany(workCertificate => workCertificate.HazardControls)
                .HasForeignKey(workCertificateHazardControl => workCertificateHazardControl.WorkCertificateId)
                .WillCascadeOnDelete(false);

            HasRequired(workCertificateHazardControl => workCertificateHazardControl.HazardControl)
                .WithMany(hazardControl => hazardControl.WorkCertificatesHazardControls)
                .HasForeignKey(workCertificateHazardControl => workCertificateHazardControl.HazardControlId)
                .WillCascadeOnDelete(false);

            ToTable("WorkCertificatesHazardControls");
        }
    }
}
