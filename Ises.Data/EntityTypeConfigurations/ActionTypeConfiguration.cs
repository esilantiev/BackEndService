using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ises.Data.EntityTypeConfigurations
{
    public class ActionTypeConfiguration : EntityTypeConfiguration<Domain.Actions.Action>
    {
        public ActionTypeConfiguration()
        {
            HasKey(action => action.Id);
  
            Property(action => action.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(action => action.Name).IsRequired();
 
            ToTable("Action");
        }
    }
}
