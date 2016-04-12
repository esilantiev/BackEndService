namespace Ises.Data.Migrations
{
    public partial class Version_106 : BaseDbMigration
    {
        public override void Upgrade()
        {
            DropForeignKey("dbo.CertificatesUsers", "UserRoleId", "dbo.UserRole");
            DropForeignKey("dbo.Area", "LocationId", "dbo.Location");
            DropForeignKey("dbo.Location", "InstallationId", "dbo.Installation");
            DropForeignKey("dbo.LeadDiscipline", "Organization_Id", "dbo.Organization");
            DropIndex("dbo.CertificatesUsers", new[] { "UserRoleId" });
            DropIndex("dbo.LeadDiscipline", new[] { "Organization_Id" });
            RenameColumn(table: "dbo.LeadDiscipline", name: "Organization_Id", newName: "OrganizationId");
            AddColumn("dbo.CertificatesUsers", "IsMandatoryAction", c => c.Boolean(nullable: false));
            AddColumn("dbo.CertificatesUsers", "ActionStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.LeadDiscipline", "OrganizationId", c => c.Long(nullable: false));
            CreateIndex("dbo.LeadDiscipline", "OrganizationId");
            AddForeignKey("dbo.Area", "LocationId", "dbo.Location", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Location", "InstallationId", "dbo.Installation", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LeadDiscipline", "OrganizationId", "dbo.Organization", "Id", cascadeDelete: true);
            DropColumn("dbo.CertificatesUsers", "UserRoleId");
            DropTable("dbo.IsolationScheme");
        }
        
        public override void Downgrade()
        {
            CreateTable(
                "dbo.IsolationScheme",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DocumentName = c.String(nullable: false),
                        Url = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CertificatesUsers", "UserRoleId", c => c.Long(nullable: false));
            DropForeignKey("dbo.LeadDiscipline", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.Location", "InstallationId", "dbo.Installation");
            DropForeignKey("dbo.Area", "LocationId", "dbo.Location");
            DropIndex("dbo.LeadDiscipline", new[] { "OrganizationId" });
            AlterColumn("dbo.LeadDiscipline", "OrganizationId", c => c.Long());
            DropColumn("dbo.CertificatesUsers", "ActionStatus");
            DropColumn("dbo.CertificatesUsers", "IsMandatoryAction");
            RenameColumn(table: "dbo.LeadDiscipline", name: "OrganizationId", newName: "Organization_Id");
            CreateIndex("dbo.LeadDiscipline", "Organization_Id");
            CreateIndex("dbo.CertificatesUsers", "UserRoleId");
            AddForeignKey("dbo.LeadDiscipline", "Organization_Id", "dbo.Organization", "Id");
            AddForeignKey("dbo.Location", "InstallationId", "dbo.Installation", "Id");
            AddForeignKey("dbo.Area", "LocationId", "dbo.Location", "Id");
            AddForeignKey("dbo.CertificatesUsers", "UserRoleId", "dbo.UserRole", "Id");
        }
    }
}
