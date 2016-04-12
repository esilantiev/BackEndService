namespace Ises.Data.Migrations
{
    public partial class Version_103 : BaseDbMigration
    {
        public override void Upgrade()
        {
            AlterColumn("dbo.UserRole", "Name", c => c.String());
        }
        
        public override void Downgrade()
        {
            AlterColumn("dbo.UserRole", "Name", c => c.String(nullable: false));
        }
    }
}
