using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Ises.Core.Utils;
using Ises.Domain.IsolationCertificates;

namespace Ises.Data.EntityTypeConfigurations
{
    public class IsolationCertificateTypeConfiguration : EntityTypeConfiguration<IsolationCertificate>
    {
        public IsolationCertificateTypeConfiguration()
        {
            HasKey(isolationCertificate => isolationCertificate.Id);

            Property(isolationCertificate => isolationCertificate.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(isolationCertificate => isolationCertificate.Type).IsRequired();
            Property(isolationCertificate => isolationCertificate.Title).IsRequired();
            Property(isolationCertificate => isolationCertificate.Description).IsRequired();
            Property(isolationCertificate => isolationCertificate.IsolationDate).IsRequired();
            Property(isolationCertificate => isolationCertificate.ContingencyPlan).IsRequired();
            Property(isolationCertificate => isolationCertificate.IsTranslationRequired).IsRequired();

            ToTable("IsolationCertificate");
        }
    }
}
