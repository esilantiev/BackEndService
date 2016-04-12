using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Ises.Domain.HistoryChanges;

namespace Ises.Data.EntityTypeConfigurations
{
    public class HistoryChangeTypeConfiguration : EntityTypeConfiguration<HistoryChange>
    {
        public HistoryChangeTypeConfiguration()
        {
            HasKey(historyChange => historyChange.Id);
  
            Property(historyChange => historyChange.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
  
            HasRequired(historyChange => historyChange.User).WithMany(u => u.UserHistoryChanges).HasForeignKey(ch => ch.UserId).WillCascadeOnDelete(false);

            ToTable("HistoryChange");
        }
    }
}
