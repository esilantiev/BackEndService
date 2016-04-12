using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Ises.Domain.LeadDisciplines;

namespace Ises.Data.EntityTypeConfigurations
{
    public class LeadDisciplineTypeConfiguration : EntityTypeConfiguration<LeadDiscipline> 
    {
        public LeadDisciplineTypeConfiguration()
        {
            HasKey(leadDiscipline => leadDiscipline.Id);
  
            Property(leadDiscipline => leadDiscipline.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(leadDiscipline => leadDiscipline.Name).IsRequired();
  
            ToTable("LeadDiscipline"); 
        }
    }
}
