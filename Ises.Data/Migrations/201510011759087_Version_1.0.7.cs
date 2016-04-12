namespace Ises.Data.Migrations
{
    public partial class Version_107 : BaseDbMigration
    {
        public override void Upgrade()
        {
            DropForeignKey("dbo.BaseCertificates", "LeadDiscipline_Id", "dbo.LeadDiscipline");
            DropForeignKey("dbo.BaseCertificates", "User_Id", "dbo.User");
            DropForeignKey("dbo.IsolationCertificate", "LeadDisciplineId", "dbo.LeadDiscipline");
            DropForeignKey("dbo.WorkCertificate", "LeadDisciplineId", "dbo.LeadDiscipline");
            DropForeignKey("dbo.IsolationCertificate", "UserId", "dbo.User");
            DropForeignKey("dbo.WorkCertificate", "UserId", "dbo.User");
            DropIndex("dbo.BaseCertificates", new[] { "LeadDiscipline_Id" });
            DropIndex("dbo.BaseCertificates", new[] { "User_Id" });
            DropIndex("dbo.IsolationCertificate", new[] { "UserId" });
            DropIndex("dbo.IsolationCertificate", new[] { "LeadDisciplineId" });
            DropIndex("dbo.WorkCertificate", new[] { "UserId" });
            DropIndex("dbo.WorkCertificate", new[] { "LeadDisciplineId" });
            RenameColumn(table: "dbo.BaseCertificates", name: "LeadDiscipline_Id", newName: "LeadDisciplineId");
            RenameColumn(table: "dbo.BaseCertificates", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.IsolationCertificate", name: "LeadDisciplineId", newName: "LeadDiscipline_Id");
            RenameColumn(table: "dbo.WorkCertificate", name: "LeadDisciplineId", newName: "LeadDiscipline_Id");
            RenameColumn(table: "dbo.IsolationCertificate", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.WorkCertificate", name: "UserId", newName: "User_Id");
            AlterColumn("dbo.BaseCertificates", "LeadDisciplineId", c => c.Long(nullable: false));
            AlterColumn("dbo.BaseCertificates", "UserId", c => c.Long(nullable: false));
            AlterColumn("dbo.IsolationCertificate", "User_Id", c => c.Long());
            AlterColumn("dbo.IsolationCertificate", "LeadDiscipline_Id", c => c.Long());
            AlterColumn("dbo.WorkCertificate", "User_Id", c => c.Long());
            AlterColumn("dbo.WorkCertificate", "LeadDiscipline_Id", c => c.Long());
            CreateIndex("dbo.BaseCertificates", "UserId");
            CreateIndex("dbo.BaseCertificates", "LeadDisciplineId");
            CreateIndex("dbo.IsolationCertificate", "User_Id");
            CreateIndex("dbo.IsolationCertificate", "LeadDiscipline_Id");
            CreateIndex("dbo.WorkCertificate", "User_Id");
            CreateIndex("dbo.WorkCertificate", "LeadDiscipline_Id");
            AddForeignKey("dbo.BaseCertificates", "LeadDisciplineId", "dbo.LeadDiscipline", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BaseCertificates", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IsolationCertificate", "LeadDiscipline_Id", "dbo.LeadDiscipline", "Id");
            AddForeignKey("dbo.WorkCertificate", "LeadDiscipline_Id", "dbo.LeadDiscipline", "Id");
            AddForeignKey("dbo.IsolationCertificate", "User_Id", "dbo.User", "Id");
            AddForeignKey("dbo.WorkCertificate", "User_Id", "dbo.User", "Id");
        }
        
        public override void Downgrade()
        {
            DropForeignKey("dbo.WorkCertificate", "User_Id", "dbo.User");
            DropForeignKey("dbo.IsolationCertificate", "User_Id", "dbo.User");
            DropForeignKey("dbo.WorkCertificate", "LeadDiscipline_Id", "dbo.LeadDiscipline");
            DropForeignKey("dbo.IsolationCertificate", "LeadDiscipline_Id", "dbo.LeadDiscipline");
            DropForeignKey("dbo.BaseCertificates", "UserId", "dbo.User");
            DropForeignKey("dbo.BaseCertificates", "LeadDisciplineId", "dbo.LeadDiscipline");
            DropIndex("dbo.WorkCertificate", new[] { "LeadDiscipline_Id" });
            DropIndex("dbo.WorkCertificate", new[] { "User_Id" });
            DropIndex("dbo.IsolationCertificate", new[] { "LeadDiscipline_Id" });
            DropIndex("dbo.IsolationCertificate", new[] { "User_Id" });
            DropIndex("dbo.BaseCertificates", new[] { "LeadDisciplineId" });
            DropIndex("dbo.BaseCertificates", new[] { "UserId" });
            AlterColumn("dbo.WorkCertificate", "LeadDiscipline_Id", c => c.Long(nullable: false));
            AlterColumn("dbo.WorkCertificate", "User_Id", c => c.Long(nullable: false));
            AlterColumn("dbo.IsolationCertificate", "LeadDiscipline_Id", c => c.Long(nullable: false));
            AlterColumn("dbo.IsolationCertificate", "User_Id", c => c.Long(nullable: false));
            AlterColumn("dbo.BaseCertificates", "UserId", c => c.Long());
            AlterColumn("dbo.BaseCertificates", "LeadDisciplineId", c => c.Long());
            RenameColumn(table: "dbo.WorkCertificate", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.IsolationCertificate", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.WorkCertificate", name: "LeadDiscipline_Id", newName: "LeadDisciplineId");
            RenameColumn(table: "dbo.IsolationCertificate", name: "LeadDiscipline_Id", newName: "LeadDisciplineId");
            RenameColumn(table: "dbo.BaseCertificates", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.BaseCertificates", name: "LeadDisciplineId", newName: "LeadDiscipline_Id");
            CreateIndex("dbo.WorkCertificate", "LeadDisciplineId");
            CreateIndex("dbo.WorkCertificate", "UserId");
            CreateIndex("dbo.IsolationCertificate", "LeadDisciplineId");
            CreateIndex("dbo.IsolationCertificate", "UserId");
            CreateIndex("dbo.BaseCertificates", "User_Id");
            CreateIndex("dbo.BaseCertificates", "LeadDiscipline_Id");
            AddForeignKey("dbo.WorkCertificate", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IsolationCertificate", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WorkCertificate", "LeadDisciplineId", "dbo.LeadDiscipline", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IsolationCertificate", "LeadDisciplineId", "dbo.LeadDiscipline", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BaseCertificates", "User_Id", "dbo.User", "Id");
            AddForeignKey("dbo.BaseCertificates", "LeadDiscipline_Id", "dbo.LeadDiscipline", "Id");
        }
    }
}
