using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Ises.Domain.Installations;

namespace Ises.Data.EntityTypeConfigurations
{
    public class InstallationTypeConfiguration : EntityTypeConfiguration<Installation>
    {
        public InstallationTypeConfiguration()
        {
            HasKey(installation => installation.Id);
  
            Property(installation => installation.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(installation => installation.Name).IsRequired();

            HasMany(installation => installation.Locations).WithRequired(location => location.Installation).WillCascadeOnDelete(false);
  
            ToTable("Installation"); 
        }
    }
}
