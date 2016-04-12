using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Ises.Domain.Positions;

namespace Ises.Data.EntityTypeConfigurations
{
    public class PositionTypeConfiguration : EntityTypeConfiguration<Position>
    {
        public PositionTypeConfiguration()
        {
            HasKey(position => position.Id);
  
            Property(position => position.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(position => position.Name).IsRequired();
            Property(position => position.LeadDisciplineId).IsRequired();
            
            HasRequired(position => position.LeadDiscipline).WithMany(organization => organization.Positions).HasForeignKey(a => a.LeadDisciplineId).WillCascadeOnDelete(false);

            ToTable("Position");
        }
    }
}
