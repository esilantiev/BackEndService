using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Ises.Domain.RolePermissions;
using Ises.Domain.Users;

namespace Ises.Data.EntityTypeConfigurations
{
    public class RolePermissionTypeConfiguration : EntityTypeConfiguration<RolePermission> 
    {
        public RolePermissionTypeConfiguration()
        {
            HasKey(rolePermission => rolePermission.Id);
 
            Property(rolePermission => rolePermission.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(rolePermission => rolePermission.Name).IsRequired();
  
            ToTable("RolePermission"); 
        }
    }
}
