using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Ises.Domain.UserRoles;
using Ises.Domain.Users;

namespace Ises.Data.EntityTypeConfigurations
{
    public class UserRoleTypeConfiguration : EntityTypeConfiguration<UserRole>
    {
        public UserRoleTypeConfiguration()
        {
            HasKey(userRole => userRole.Id);
  
            Property(userRole => userRole.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(userRole => userRole.Name).IsRequired();
            Property(userRole => userRole.ShortEnglishName);

            HasMany(userRole => userRole.RolePermissions)
                .WithMany(rolePermission => rolePermission.UserRoles)
                .Map(m =>
                        {
                            m.MapLeftKey("UserRoleId");
                            m.MapRightKey("RolePermissionId");
                            m.ToTable("UserRolesRolePermissions");
                        });
 
            ToTable("UserRole");
        }
    }
}
