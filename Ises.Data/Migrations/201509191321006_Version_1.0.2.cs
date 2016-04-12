namespace Ises.Data.Migrations
{
    public partial class Version_102 : BaseDbMigration
    {
        public override void Upgrade()
        {
            AlterColumn("dbo.User", "Name", c => c.String());
        }
        
        public override void Downgrade()
        {
            AlterColumn("dbo.User", "Name", c => c.String(nullable: false));
        }
    }
}
