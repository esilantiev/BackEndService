using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Ises.Domain.Hazards;

namespace Ises.Data.EntityTypeConfigurations
{
    public class HazardTypeConfiguration : EntityTypeConfiguration<Hazard> 
    {
        public HazardTypeConfiguration()
        {
            HasKey(hazard => hazard.Id);
  
            Property(hazard => hazard.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(hazard => hazard.Name).IsRequired();
            Property(hazard => hazard.HazardGroupId).IsRequired();
  
            HasRequired(hazard => hazard.HazardGroup).WithMany(hazardGroup => hazardGroup.Hazards).HasForeignKey(hazard => hazard.HazardGroupId).WillCascadeOnDelete(false);

            ToTable("Hazard");
        }
    }
}
