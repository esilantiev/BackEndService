namespace Ises.Data.Migrations
{
    public partial class Version_104 : BaseDbMigration
    {
        public override void Upgrade()
        {
            AlterColumn("dbo.User", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.UserRole", "Name", c => c.String(nullable: false));
        }
        
        public override void Downgrade()
        {
            AlterColumn("dbo.UserRole", "Name", c => c.String());
            AlterColumn("dbo.User", "Name", c => c.String());
        }
    }
}
