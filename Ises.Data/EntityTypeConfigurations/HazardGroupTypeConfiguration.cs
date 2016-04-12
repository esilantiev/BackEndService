using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Ises.Domain.HazardGroups;

namespace Ises.Data.EntityTypeConfigurations
{
    public class HazardGroupTypeConfiguration : EntityTypeConfiguration<HazardGroup> 
    {
        public HazardGroupTypeConfiguration()
        {
            HasKey(hazardGroup => hazardGroup.Id);
  
            Property(hazardGroup => hazardGroup.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(hazardGroup => hazardGroup.Name).IsRequired();
  
            ToTable("HazardGroup"); 
        }
    }
}
