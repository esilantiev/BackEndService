using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Ises.Domain.ExternalDocuments;

namespace Ises.Data.EntityTypeConfigurations
{
    public class ExternalDocumentTypeConfiguration : EntityTypeConfiguration<ExternalDocument>
    {
        public ExternalDocumentTypeConfiguration()
        {
            Property(externalDocument => externalDocument.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
 
            ToTable("ExternalDocument");
        }
    }
}
