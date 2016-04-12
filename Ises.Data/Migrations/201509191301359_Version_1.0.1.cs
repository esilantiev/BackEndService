namespace Ises.Data.Migrations
{
    public partial class Version_101 : BaseDbMigration
    {
        public override void Upgrade()
        {
            RenameTable(name: "dbo.AreaTypes", newName: "AreaType");
            RenameTable(name: "dbo.WorkCertificateCategories", newName: "WorkCertificateCategory");
            RenameColumn(table: "dbo.User", name: "ContactDetails_Phone", newName: "Phone");
            RenameColumn(table: "dbo.User", name: "ContactDetails_Email", newName: "Email");
            RenameColumn(table: "dbo.User", name: "ContactDetails_PersonalPhone", newName: "PersonalPhone");
            RenameColumn(table: "dbo.User", name: "ContactDetails_PersonalEmail", newName: "PersonalEmail");
            RenameColumn(table: "dbo.User", name: "ContactDetails_HomeCity", newName: "HomeCity");
        }
        
        public override void Downgrade()
        {
            RenameColumn(table: "dbo.User", name: "HomeCity", newName: "ContactDetails_HomeCity");
            RenameColumn(table: "dbo.User", name: "PersonalEmail", newName: "ContactDetails_PersonalEmail");
            RenameColumn(table: "dbo.User", name: "PersonalPhone", newName: "ContactDetails_PersonalPhone");
            RenameColumn(table: "dbo.User", name: "Email", newName: "ContactDetails_Email");
            RenameColumn(table: "dbo.User", name: "Phone", newName: "ContactDetails_Phone");
            RenameTable(name: "dbo.WorkCertificateCategory", newName: "WorkCertificateCategories");
            RenameTable(name: "dbo.AreaType", newName: "AreaTypes");
        }
    }
}
