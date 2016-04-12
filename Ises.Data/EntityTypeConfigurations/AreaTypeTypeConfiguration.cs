using System.Data.Entity.ModelConfiguration;
using Ises.Domain.AreaTypes;

namespace Ises.Data.EntityTypeConfigurations
{
    public class AreaTypeTypeConfiguration : EntityTypeConfiguration<AreaType>
    {
        public AreaTypeTypeConfiguration()
        {
            ToTable("AreaType");
        }
    }
}
