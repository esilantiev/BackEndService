using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Ises.Domain.Organizations;

namespace Ises.Data.EntityTypeConfigurations
{
    public class OrganizationTypeConfiguration : EntityTypeConfiguration<Organization>
    {
        public OrganizationTypeConfiguration()
        {
            HasKey(organization => organization.Id);

            Property(organization => organization.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(organization => organization.Name).IsRequired();
  
            ToTable("Organization");
        }
    }
}
