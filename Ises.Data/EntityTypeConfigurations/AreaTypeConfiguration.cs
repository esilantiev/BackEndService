using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Ises.Domain.Areas;
using TrackerEnabledDbContext.Common.Configuration;

namespace Ises.Data.EntityTypeConfigurations
{
    public class AreaTypeConfiguration : EntityTypeConfiguration<Area>
    {
        public AreaTypeConfiguration()
        {
            HasKey(area => area.Id);
  
            Property(area => area.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(area => area.Name).IsRequired();
            Property(area => area.LocationId).IsRequired();
            EntityTracker
            .TrackAllProperties<Area>();
           
            HasRequired(area => area.Location).WithMany(location => location.Areas).HasForeignKey(area => area.LocationId).WillCascadeOnDelete(true);

            HasMany(area => area.WorkCertificateAreas)
               .WithRequired()
               .HasForeignKey(cp => cp.AreaId);

            ToTable("Area");
        }
    }
}
