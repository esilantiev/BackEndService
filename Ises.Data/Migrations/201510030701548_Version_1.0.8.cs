namespace Ises.Data.Migrations
{
    public partial class Version_108 : BaseDbMigration
    {
        public override void Upgrade()
        {
            DropForeignKey("dbo.IsolationCertificate", "User_Id", "dbo.User");
            DropForeignKey("dbo.IsolationCertificate", "LeadDiscipline_Id", "dbo.LeadDiscipline");
            DropForeignKey("dbo.WorkCertificate", "User_Id", "dbo.User");
            DropForeignKey("dbo.WorkCertificate", "LeadDiscipline_Id", "dbo.LeadDiscipline");
            DropForeignKey("dbo.ExternalDocument", "BaseCertificate_Id", "dbo.BaseCertificates");
            DropIndex("dbo.ExternalDocument", new[] { "BaseCertificate_Id" });
            DropIndex("dbo.IsolationCertificate", new[] { "User_Id" });
            DropIndex("dbo.IsolationCertificate", new[] { "LeadDiscipline_Id" });
            DropIndex("dbo.WorkCertificate", new[] { "User_Id" });
            DropIndex("dbo.WorkCertificate", new[] { "LeadDiscipline_Id" });
            RenameColumn(table: "dbo.ExternalDocument", name: "BaseCertificate_Id", newName: "BaseCertificateId");
            AlterColumn("dbo.ExternalDocument", "BaseCertificateId", c => c.Long(nullable: false));
            CreateIndex("dbo.ExternalDocument", "BaseCertificateId");
            AddForeignKey("dbo.ExternalDocument", "BaseCertificateId", "dbo.BaseCertificates", "Id", cascadeDelete: true);
            DropColumn("dbo.IsolationCertificate", "User_Id");
            DropColumn("dbo.IsolationCertificate", "LeadDiscipline_Id");
            DropColumn("dbo.WorkCertificate", "User_Id");
            DropColumn("dbo.WorkCertificate", "LeadDiscipline_Id");
            DropColumn("dbo.ExternalDocument", "CertificateId");
        }
        
        public override void Downgrade()
        {
            AddColumn("dbo.ExternalDocument", "CertificateId", c => c.Long(nullable: false));
            AddColumn("dbo.WorkCertificate", "LeadDiscipline_Id", c => c.Long());
            AddColumn("dbo.WorkCertificate", "User_Id", c => c.Long());
            AddColumn("dbo.IsolationCertificate", "LeadDiscipline_Id", c => c.Long());
            AddColumn("dbo.IsolationCertificate", "User_Id", c => c.Long());
            DropForeignKey("dbo.ExternalDocument", "BaseCertificateId", "dbo.BaseCertificates");
            DropIndex("dbo.ExternalDocument", new[] { "BaseCertificateId" });
            AlterColumn("dbo.ExternalDocument", "BaseCertificateId", c => c.Long());
            RenameColumn(table: "dbo.ExternalDocument", name: "BaseCertificateId", newName: "BaseCertificate_Id");
            CreateIndex("dbo.WorkCertificate", "LeadDiscipline_Id");
            CreateIndex("dbo.WorkCertificate", "User_Id");
            CreateIndex("dbo.IsolationCertificate", "LeadDiscipline_Id");
            CreateIndex("dbo.IsolationCertificate", "User_Id");
            CreateIndex("dbo.ExternalDocument", "BaseCertificate_Id");
            AddForeignKey("dbo.ExternalDocument", "BaseCertificate_Id", "dbo.BaseCertificates", "Id");
            AddForeignKey("dbo.WorkCertificate", "LeadDiscipline_Id", "dbo.LeadDiscipline", "Id");
            AddForeignKey("dbo.WorkCertificate", "User_Id", "dbo.User", "Id");
            AddForeignKey("dbo.IsolationCertificate", "LeadDiscipline_Id", "dbo.LeadDiscipline", "Id");
            AddForeignKey("dbo.IsolationCertificate", "User_Id", "dbo.User", "Id");
        }
    }
}
