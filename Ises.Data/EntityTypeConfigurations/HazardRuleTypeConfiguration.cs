using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Ises.Domain.HazardRules;
using Ises.Domain.Hazards;

namespace Ises.Data.EntityTypeConfigurations
{
    public class HazardRuleTypeConfiguration : EntityTypeConfiguration<HazardRule>
    {
        public HazardRuleTypeConfiguration()
        {
            HasKey(h => h.Id);
 
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.WorkCertificateCategoryId).IsRequired().HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("IX_WorkCertificateCategoryIdAreaTypeId", 1) { IsUnique = true }));
            Property(t => t.AreaTypeId).IsRequired().HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("IX_WorkCertificateCategoryIdAreaTypeId", 2) { IsUnique = true })); ;
  
            ToTable("HazardRule");
        }
    }
}
