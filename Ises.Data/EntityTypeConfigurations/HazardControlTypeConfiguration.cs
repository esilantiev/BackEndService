using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Ises.Domain.HazardControls;
using Ises.Domain.Hazards;

namespace Ises.Data.EntityTypeConfigurations
{
    public class HazardControlTypeConfiguration : EntityTypeConfiguration<HazardControl>
    {
        public HazardControlTypeConfiguration()
        {
            HasKey(hc => hc.Id);
  
            Property(hc => hc.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(hc => hc.Name).IsRequired();
            Property(hc => hc.HazardId).IsRequired();
 
            HasRequired(h => h.Hazard).WithMany(h => h.HazardControls).HasForeignKey(t => t.HazardId).WillCascadeOnDelete(false);
  
            HasMany(c => c.HazardRules).
                WithMany(r => r.HazardControls).
                Map(
                    m =>
                    {
                        m.MapLeftKey("HazardControlId");
                        m.MapRightKey("HazardRuleId");
                        m.ToTable("HazardControlsHazardRules");
                    });

            ToTable("HazardControl");
        }
    }
}
