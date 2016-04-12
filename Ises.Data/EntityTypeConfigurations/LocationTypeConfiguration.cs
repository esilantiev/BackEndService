using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Ises.Domain.Locations;
using TrackerEnabledDbContext.Common.Configuration;

namespace Ises.Data.EntityTypeConfigurations
{
    public class LocationTypeConfiguration : EntityTypeConfiguration<Location>
    {
        public LocationTypeConfiguration()
        {
            HasKey(location => location.Id);
  
            Property(location => location.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(location => location.Name).IsRequired();
            Property(location => location.InstallationId).IsRequired();
            EntityTracker.TrackAllProperties<Location>();

            HasRequired(location => location.Installation).WithMany(installation => installation.Locations).WillCascadeOnDelete(false);
            
            ToTable("Location");
        }
    }
}
