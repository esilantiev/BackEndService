namespace Ises.Data.Migrations
{
    public partial class Version_109 : BaseDbMigration
    {
        public override void Upgrade()
        {
            CreateTable(
                "dbo.IsolationTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IsolationMethods",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsolationTypeId = c.Long(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IsolationTypes", t => t.IsolationTypeId, cascadeDelete: true)
                .Index(t => t.IsolationTypeId);
            
            CreateTable(
                "dbo.IsolationStates",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsolationMethodId = c.Long(nullable: false),
                        IsolationStateId = c.Long(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IsolationMethods", t => t.IsolationMethodId, cascadeDelete: true)
                .ForeignKey("dbo.IsolationStates", t => t.IsolationStateId)
                .Index(t => t.IsolationMethodId)
                .Index(t => t.IsolationStateId);
            
            AddColumn("dbo.IsolationPoint", "IsolationTypeId", c => c.Long(nullable: false));
            AlterColumn("dbo.IsolationPoint", "LoLc", c => c.Int());
            CreateIndex("dbo.IsolationPoint", "IsolationTypeId");
            AddForeignKey("dbo.IsolationPoint", "IsolationTypeId", "dbo.IsolationTypes", "Id", cascadeDelete: true);
            DropColumn("dbo.IsolationPoint", "Type");
            DropColumn("dbo.IsolationPoint", "Method");
            DropColumn("dbo.IsolationPoint", "IsolatedState");
            DropColumn("dbo.IsolationPoint", "DeIsolatedState");
        }
        
        public override void Downgrade()
        {
            AddColumn("dbo.IsolationPoint", "DeIsolatedState", c => c.String());
            AddColumn("dbo.IsolationPoint", "IsolatedState", c => c.String());
            AddColumn("dbo.IsolationPoint", "Method", c => c.String(nullable: false));
            AddColumn("dbo.IsolationPoint", "Type", c => c.String(nullable: false));
            DropForeignKey("dbo.IsolationPoint", "IsolationTypeId", "dbo.IsolationTypes");
            DropForeignKey("dbo.IsolationMethods", "IsolationTypeId", "dbo.IsolationTypes");
            DropForeignKey("dbo.IsolationStates", "IsolationStateId", "dbo.IsolationStates");
            DropForeignKey("dbo.IsolationStates", "IsolationMethodId", "dbo.IsolationMethods");
            DropIndex("dbo.IsolationStates", new[] { "IsolationStateId" });
            DropIndex("dbo.IsolationStates", new[] { "IsolationMethodId" });
            DropIndex("dbo.IsolationMethods", new[] { "IsolationTypeId" });
            DropIndex("dbo.IsolationPoint", new[] { "IsolationTypeId" });
            AlterColumn("dbo.IsolationPoint", "LoLc", c => c.String());
            DropColumn("dbo.IsolationPoint", "IsolationTypeId");
            DropTable("dbo.IsolationStates");
            DropTable("dbo.IsolationMethods");
            DropTable("dbo.IsolationTypes");
        }
    }
}
