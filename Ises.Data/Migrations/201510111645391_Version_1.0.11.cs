namespace Ises.Data.Migrations
{
    public partial class Version_1011 : BaseDbMigration
    {
        public override void Upgrade()
        {
            DropForeignKey("dbo.Location", "InstallationId", "dbo.Installation");
            AddForeignKey("dbo.Location", "InstallationId", "dbo.Installation", "Id");
        }
        
        public override void Downgrade()
        {
            DropForeignKey("dbo.Location", "InstallationId", "dbo.Installation");
            AddForeignKey("dbo.Location", "InstallationId", "dbo.Installation", "Id", cascadeDelete: true);
        }
    }
}
