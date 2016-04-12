using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Ises.Domain.IsolationPoints;

namespace Ises.Data.EntityTypeConfigurations
{
    public class IsolationPointTypeConfiguration : EntityTypeConfiguration<IsolationPoint>
    {
        public IsolationPointTypeConfiguration()
        {
            HasKey(isolationPoint => isolationPoint.Id);

            Property(isolationPoint => isolationPoint.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(isolationPoint => isolationPoint.TagNumber).IsRequired();
            Property(isolationPoint => isolationPoint.Description).IsRequired();
            Property(isolationPoint => isolationPoint.Comment).IsOptional();
            Property(isolationPoint => isolationPoint.LoLc).IsOptional();

            HasRequired(isolationPoint => isolationPoint.IsolationCertificate)
                .WithMany(isolationCertificate => isolationCertificate.IsolationPoints)
                .HasForeignKey(isolationPoint => isolationPoint.IsolationCertificateId)
                .WillCascadeOnDelete(false);

            ToTable("IsolationPoint");
        }
    }
}
