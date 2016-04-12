using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Ises.Domain.Users;

namespace Ises.Data.EntityTypeConfigurations
{
    public class UserTypeConfiguration : EntityTypeConfiguration<User>
    {
        public UserTypeConfiguration()
        {
            HasKey(user => user.Id);

            Property(user => user.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(user => user.Name).IsRequired();
            Property(user => user.PositionId).IsRequired();
            Property(user => user.ContactDetails.Email).HasColumnName("Email");
            Property(user => user.ContactDetails.HomeCity).HasColumnName("HomeCity");
            Property(user => user.ContactDetails.PersonalEmail).HasColumnName("PersonalEmail");
            Property(user => user.ContactDetails.PersonalPhone).HasColumnName("PersonalPhone");
            Property(user => user.ContactDetails.Phone).HasColumnName("Phone");
            Property(user => user.PersonImage).HasColumnType("image");
            Property(user => user.Login).IsRequired();

            HasRequired(user => user.Position).WithMany(position => position.Users).HasForeignKey(user => user.PositionId).WillCascadeOnDelete(false);
            HasRequired(user => user.Installation).WithMany(installation => installation.Users).HasForeignKey(user => user.InstallationId).WillCascadeOnDelete(false);

            HasMany(user => user.UserRoles)
                .WithMany(userRole => userRole.Users)
                .Map(m => {
                            m.MapLeftKey("UserId");
                            m.MapRightKey("UserRoleId");
                            m.ToTable("UsersUserRoles");
                          });

            HasMany(user => user.Areas).
            WithMany(r => r.Users).
            Map(
                m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("AreaId");
                    m.ToTable("UsersAreas");
                });

            HasOptional(u => u.Manager).
              WithMany().
              HasForeignKey(u => u.ManagerId);

            HasMany(u => u.Favorites).
               WithMany().
               Map(
                   m =>
                   {
                       m.MapLeftKey("UserId");
                       m.MapRightKey("FavoriteUserId");
                       m.ToTable("UsersFavorites");
                   });

            ToTable("User");
        }
    }
}
