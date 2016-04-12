using System.Data.Entity.ModelConfiguration;
using Ises.Domain.CertificatesUsers;

namespace Ises.Data.EntityTypeConfigurations
{
    public class CertificateUserTypeConfiguration : EntityTypeConfiguration<CertificateUser>
    {
        public CertificateUserTypeConfiguration()
        {
            HasRequired(certificateUser => certificateUser.User)
                .WithMany(user => user.CertificateUsers)
                .HasForeignKey(certificateUser => certificateUser.UserId)
                .WillCascadeOnDelete(false);

            HasRequired(certificateUser => certificateUser.Action)
                .WithMany(action => action.CertificateUsers)
                .HasForeignKey(certificateUser => certificateUser.ActionId)
                .WillCascadeOnDelete(false);

            ToTable("CertificatesUsers");
        }
    }
}
