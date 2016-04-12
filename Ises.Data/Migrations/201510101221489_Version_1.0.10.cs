namespace Ises.Data.Migrations
{
    public partial class Version_1010 : BaseDbMigration
    {
        public override void Upgrade()
        {
            AddColumn("dbo.Area", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Downgrade()
        {
            DropColumn("dbo.Area", "RowVersion");
        }
    }
}
