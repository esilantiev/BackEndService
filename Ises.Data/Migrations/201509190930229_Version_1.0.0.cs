namespace Ises.Data.Migrations
{
    public partial class Version_100 : BaseDbMigration
    {
        public override void Upgrade()
        {
            CreateTable(
                "dbo.Action",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CertificatesUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BaseCertificateId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        ActionId = c.Long(nullable: false),
                        UserRoleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Action", t => t.ActionId)
                .ForeignKey("dbo.BaseCertificates", t => t.BaseCertificateId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.UserRole", t => t.UserRoleId)
                .Index(t => t.BaseCertificateId)
                .Index(t => t.UserId)
                .Index(t => t.ActionId)
                .Index(t => t.UserRoleId);
            
            CreateTable(
                "dbo.BaseCertificates",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        TagNumber = c.String(),
                        IsTranslationRequired = c.Int(nullable: false),
                        LeadDiscipline_Id = c.Long(),
                        User_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LeadDiscipline", t => t.LeadDiscipline_Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.LeadDiscipline_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ExternalDocument",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CertificateId = c.Long(nullable: false),
                        DocumentName = c.String(),
                        Url = c.String(),
                        BaseCertificate_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseCertificates", t => t.BaseCertificate_Id)
                .Index(t => t.BaseCertificate_Id);
            
            CreateTable(
                "dbo.LeadDiscipline",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Organization_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organization", t => t.Organization_Id)
                .Index(t => t.Organization_Id);
            
            CreateTable(
                "dbo.IsolationPoint",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsolationCertificateId = c.Long(nullable: false),
                        TagNumber = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        Method = c.String(nullable: false),
                        Comment = c.String(),
                        LoLc = c.String(),
                        IsolatedState = c.String(),
                        DeIsolatedState = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IsolationCertificate", t => t.IsolationCertificateId)
                .Index(t => t.IsolationCertificateId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ManagerId = c.Long(),
                        InstallationId = c.Long(nullable: false),
                        PositionId = c.Long(nullable: false),
                        Name = c.String(nullable: false),
                        Login = c.String(nullable: false),
                        PersonImage = c.Binary(storeType: "image"),
                        IsAtWork = c.Int(nullable: false),
                        Shift = c.Int(nullable: false),
                        ContactDetails_Phone = c.String(),
                        ContactDetails_Email = c.String(),
                        ContactDetails_PersonalPhone = c.String(),
                        ContactDetails_PersonalEmail = c.String(),
                        ContactDetails_HomeCity = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Installation", t => t.InstallationId)
                .ForeignKey("dbo.User", t => t.ManagerId)
                .ForeignKey("dbo.Position", t => t.PositionId)
                .Index(t => t.ManagerId)
                .Index(t => t.InstallationId)
                .Index(t => t.PositionId);
            
            CreateTable(
                "dbo.Area",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LocationId = c.Long(nullable: false),
                        AreaTypeId = c.Long(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AreaTypes", t => t.AreaTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Location", t => t.LocationId)
                .Index(t => t.LocationId)
                .Index(t => t.AreaTypeId);
            
            CreateTable(
                "dbo.AreaTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        InstallationId = c.Long(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Installation", t => t.InstallationId)
                .Index(t => t.InstallationId);
            
            CreateTable(
                "dbo.Installation",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkCertificateAreas",
                c => new
                    {
                        WorkCertificateId = c.Long(nullable: false),
                        AreaId = c.Long(nullable: false),
                        IsMainArea = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkCertificateId, t.AreaId })
                .ForeignKey("dbo.Area", t => t.AreaId, cascadeDelete: true)
                .ForeignKey("dbo.WorkCertificate", t => t.WorkCertificateId)
                .Index(t => t.WorkCertificateId)
                .Index(t => t.AreaId);
            
            CreateTable(
                "dbo.WorkCertificatesHazardControls",
                c => new
                    {
                        WorkCertificateId = c.Long(nullable: false),
                        HazardControlId = c.Long(nullable: false),
                        HazardControlType = c.Int(nullable: false),
                        IsAutomaticallyCalculated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkCertificateId, t.HazardControlId })
                .ForeignKey("dbo.HazardControl", t => t.HazardControlId)
                .ForeignKey("dbo.WorkCertificate", t => t.WorkCertificateId)
                .Index(t => t.WorkCertificateId)
                .Index(t => t.HazardControlId);
            
            CreateTable(
                "dbo.HazardControl",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        HazardId = c.Long(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hazard", t => t.HazardId)
                .Index(t => t.HazardId);
            
            CreateTable(
                "dbo.Hazard",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        HazardGroupId = c.Long(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HazardGroup", t => t.HazardGroupId)
                .Index(t => t.HazardGroupId);
            
            CreateTable(
                "dbo.HazardGroup",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkCertificatesHazards",
                c => new
                    {
                        WorkCertificateId = c.Long(nullable: false),
                        HazardId = c.Long(nullable: false),
                        InitialRisk = c.Int(),
                        ResidualRisk = c.Int(),
                        InitialRiskName = c.String(),
                        ResidualRiskName = c.String(),
                        Alarp = c.Boolean(),
                        IsAutomaticAlarp = c.Boolean(),
                    })
                .PrimaryKey(t => new { t.WorkCertificateId, t.HazardId })
                .ForeignKey("dbo.Hazard", t => t.HazardId)
                .ForeignKey("dbo.WorkCertificate", t => t.WorkCertificateId)
                .Index(t => t.WorkCertificateId)
                .Index(t => t.HazardId);
            
            CreateTable(
                "dbo.HazardRule",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        WorkCertificateCategoryId = c.Long(nullable: false),
                        AreaTypeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AreaTypes", t => t.AreaTypeId, cascadeDelete: true)
                .ForeignKey("dbo.WorkCertificateCategories", t => t.WorkCertificateCategoryId, cascadeDelete: true)
                .Index(t => new { t.WorkCertificateCategoryId, t.AreaTypeId }, unique: true, name: "IX_WorkCertificateCategoryIdAreaTypeId");
            
            CreateTable(
                "dbo.WorkCertificateCategories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkCertificatesWorkCertificates",
                c => new
                    {
                        FirstWorkCertificateId = c.Long(nullable: false),
                        SecondWorkCertificateId = c.Long(nullable: false),
                        ConnectionType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FirstWorkCertificateId, t.SecondWorkCertificateId, t.ConnectionType })
                .ForeignKey("dbo.WorkCertificate", t => t.FirstWorkCertificateId)
                .ForeignKey("dbo.WorkCertificate", t => t.SecondWorkCertificateId)
                .Index(t => t.FirstWorkCertificateId)
                .Index(t => t.SecondWorkCertificateId);
            
            CreateTable(
                "dbo.Position",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LeadDisciplineId = c.Long(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LeadDiscipline", t => t.LeadDisciplineId)
                .Index(t => t.LeadDisciplineId);
            
            CreateTable(
                "dbo.HistoryChange",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        UserRoleId = c.Long(nullable: false),
                        EntityId = c.Long(nullable: false),
                        EntityType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.UserRole", t => t.UserRoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.UserRoleId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ShortEnglishName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RolePermission",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Organization",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IsolationScheme",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DocumentName = c.String(nullable: false),
                        Url = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HazardControlsHazardRules",
                c => new
                    {
                        HazardControlId = c.Long(nullable: false),
                        HazardRuleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.HazardControlId, t.HazardRuleId })
                .ForeignKey("dbo.HazardControl", t => t.HazardControlId, cascadeDelete: true)
                .ForeignKey("dbo.HazardRule", t => t.HazardRuleId, cascadeDelete: true)
                .Index(t => t.HazardControlId)
                .Index(t => t.HazardRuleId);
            
            CreateTable(
                "dbo.WorkCertificatesIsolationCertificates",
                c => new
                    {
                        WorkCertificateId = c.Long(nullable: false),
                        IsolationCertificateId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkCertificateId, t.IsolationCertificateId })
                .ForeignKey("dbo.WorkCertificate", t => t.WorkCertificateId)
                .ForeignKey("dbo.IsolationCertificate", t => t.IsolationCertificateId)
                .Index(t => t.WorkCertificateId)
                .Index(t => t.IsolationCertificateId);
            
            CreateTable(
                "dbo.UsersAreas",
                c => new
                    {
                        UserId = c.Long(nullable: false),
                        AreaId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.AreaId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Area", t => t.AreaId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.AreaId);
            
            CreateTable(
                "dbo.UsersFavorites",
                c => new
                    {
                        UserId = c.Long(nullable: false),
                        FavoriteUserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.FavoriteUserId })
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.User", t => t.FavoriteUserId)
                .Index(t => t.UserId)
                .Index(t => t.FavoriteUserId);
            
            CreateTable(
                "dbo.UserRolesRolePermissions",
                c => new
                    {
                        UserRoleId = c.Long(nullable: false),
                        RolePermissionId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRoleId, t.RolePermissionId })
                .ForeignKey("dbo.UserRole", t => t.UserRoleId, cascadeDelete: true)
                .ForeignKey("dbo.RolePermission", t => t.RolePermissionId, cascadeDelete: true)
                .Index(t => t.UserRoleId)
                .Index(t => t.RolePermissionId);
            
            CreateTable(
                "dbo.UsersUserRoles",
                c => new
                    {
                        UserId = c.Long(nullable: false),
                        UserRoleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.UserRoleId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.UserRole", t => t.UserRoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.UserRoleId);
            
            CreateTable(
                "dbo.IsolationCertificate",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        LeadDisciplineId = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        IsolationDate = c.DateTime(nullable: false),
                        ContingencyPlan = c.String(nullable: false),
                        MaintenanceNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseCertificates", t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.LeadDiscipline", t => t.LeadDisciplineId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.UserId)
                .Index(t => t.LeadDisciplineId);
            
            CreateTable(
                "dbo.WorkCertificate",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        LeadDisciplineId = c.Long(nullable: false),
                        WorkCertificateCategoryId = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        EquipmentDescription = c.String(),
                        IsolationType = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        WorkersCount = c.Int(nullable: false),
                        WorkHoursCount = c.Int(nullable: false),
                        MaintenanceNumber = c.String(),
                        IsRal2Required = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseCertificates", t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.LeadDiscipline", t => t.LeadDisciplineId, cascadeDelete: true)
                .ForeignKey("dbo.WorkCertificateCategories", t => t.WorkCertificateCategoryId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.UserId)
                .Index(t => t.LeadDisciplineId)
                .Index(t => t.WorkCertificateCategoryId);
            
        }
        
        public override void Downgrade()
        {
            DropForeignKey("dbo.WorkCertificate", "WorkCertificateCategoryId", "dbo.WorkCertificateCategories");
            DropForeignKey("dbo.WorkCertificate", "LeadDisciplineId", "dbo.LeadDiscipline");
            DropForeignKey("dbo.WorkCertificate", "UserId", "dbo.User");
            DropForeignKey("dbo.WorkCertificate", "Id", "dbo.BaseCertificates");
            DropForeignKey("dbo.IsolationCertificate", "LeadDisciplineId", "dbo.LeadDiscipline");
            DropForeignKey("dbo.IsolationCertificate", "UserId", "dbo.User");
            DropForeignKey("dbo.IsolationCertificate", "Id", "dbo.BaseCertificates");
            DropForeignKey("dbo.LeadDiscipline", "Organization_Id", "dbo.Organization");
            DropForeignKey("dbo.CertificatesUsers", "UserRoleId", "dbo.UserRole");
            DropForeignKey("dbo.CertificatesUsers", "UserId", "dbo.User");
            DropForeignKey("dbo.BaseCertificates", "User_Id", "dbo.User");
            DropForeignKey("dbo.BaseCertificates", "LeadDiscipline_Id", "dbo.LeadDiscipline");
            DropForeignKey("dbo.UsersUserRoles", "UserRoleId", "dbo.UserRole");
            DropForeignKey("dbo.UsersUserRoles", "UserId", "dbo.User");
            DropForeignKey("dbo.HistoryChange", "UserRoleId", "dbo.UserRole");
            DropForeignKey("dbo.UserRolesRolePermissions", "RolePermissionId", "dbo.RolePermission");
            DropForeignKey("dbo.UserRolesRolePermissions", "UserRoleId", "dbo.UserRole");
            DropForeignKey("dbo.HistoryChange", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "PositionId", "dbo.Position");
            DropForeignKey("dbo.Position", "LeadDisciplineId", "dbo.LeadDiscipline");
            DropForeignKey("dbo.User", "ManagerId", "dbo.User");
            DropForeignKey("dbo.User", "InstallationId", "dbo.Installation");
            DropForeignKey("dbo.UsersFavorites", "FavoriteUserId", "dbo.User");
            DropForeignKey("dbo.UsersFavorites", "UserId", "dbo.User");
            DropForeignKey("dbo.UsersAreas", "AreaId", "dbo.Area");
            DropForeignKey("dbo.UsersAreas", "UserId", "dbo.User");
            DropForeignKey("dbo.WorkCertificatesWorkCertificates", "SecondWorkCertificateId", "dbo.WorkCertificate");
            DropForeignKey("dbo.WorkCertificatesWorkCertificates", "FirstWorkCertificateId", "dbo.WorkCertificate");
            DropForeignKey("dbo.WorkCertificateAreas", "WorkCertificateId", "dbo.WorkCertificate");
            DropForeignKey("dbo.WorkCertificatesIsolationCertificates", "IsolationCertificateId", "dbo.IsolationCertificate");
            DropForeignKey("dbo.WorkCertificatesIsolationCertificates", "WorkCertificateId", "dbo.WorkCertificate");
            DropForeignKey("dbo.WorkCertificatesHazardControls", "WorkCertificateId", "dbo.WorkCertificate");
            DropForeignKey("dbo.WorkCertificatesHazardControls", "HazardControlId", "dbo.HazardControl");
            DropForeignKey("dbo.HazardControlsHazardRules", "HazardRuleId", "dbo.HazardRule");
            DropForeignKey("dbo.HazardControlsHazardRules", "HazardControlId", "dbo.HazardControl");
            DropForeignKey("dbo.HazardRule", "WorkCertificateCategoryId", "dbo.WorkCertificateCategories");
            DropForeignKey("dbo.HazardRule", "AreaTypeId", "dbo.AreaTypes");
            DropForeignKey("dbo.HazardControl", "HazardId", "dbo.Hazard");
            DropForeignKey("dbo.WorkCertificatesHazards", "WorkCertificateId", "dbo.WorkCertificate");
            DropForeignKey("dbo.WorkCertificatesHazards", "HazardId", "dbo.Hazard");
            DropForeignKey("dbo.Hazard", "HazardGroupId", "dbo.HazardGroup");
            DropForeignKey("dbo.WorkCertificateAreas", "AreaId", "dbo.Area");
            DropForeignKey("dbo.Area", "LocationId", "dbo.Location");
            DropForeignKey("dbo.Location", "InstallationId", "dbo.Installation");
            DropForeignKey("dbo.Area", "AreaTypeId", "dbo.AreaTypes");
            DropForeignKey("dbo.IsolationPoint", "IsolationCertificateId", "dbo.IsolationCertificate");
            DropForeignKey("dbo.ExternalDocument", "BaseCertificate_Id", "dbo.BaseCertificates");
            DropForeignKey("dbo.CertificatesUsers", "BaseCertificateId", "dbo.BaseCertificates");
            DropForeignKey("dbo.CertificatesUsers", "ActionId", "dbo.Action");
            DropIndex("dbo.WorkCertificate", new[] { "WorkCertificateCategoryId" });
            DropIndex("dbo.WorkCertificate", new[] { "LeadDisciplineId" });
            DropIndex("dbo.WorkCertificate", new[] { "UserId" });
            DropIndex("dbo.WorkCertificate", new[] { "Id" });
            DropIndex("dbo.IsolationCertificate", new[] { "LeadDisciplineId" });
            DropIndex("dbo.IsolationCertificate", new[] { "UserId" });
            DropIndex("dbo.IsolationCertificate", new[] { "Id" });
            DropIndex("dbo.UsersUserRoles", new[] { "UserRoleId" });
            DropIndex("dbo.UsersUserRoles", new[] { "UserId" });
            DropIndex("dbo.UserRolesRolePermissions", new[] { "RolePermissionId" });
            DropIndex("dbo.UserRolesRolePermissions", new[] { "UserRoleId" });
            DropIndex("dbo.UsersFavorites", new[] { "FavoriteUserId" });
            DropIndex("dbo.UsersFavorites", new[] { "UserId" });
            DropIndex("dbo.UsersAreas", new[] { "AreaId" });
            DropIndex("dbo.UsersAreas", new[] { "UserId" });
            DropIndex("dbo.WorkCertificatesIsolationCertificates", new[] { "IsolationCertificateId" });
            DropIndex("dbo.WorkCertificatesIsolationCertificates", new[] { "WorkCertificateId" });
            DropIndex("dbo.HazardControlsHazardRules", new[] { "HazardRuleId" });
            DropIndex("dbo.HazardControlsHazardRules", new[] { "HazardControlId" });
            DropIndex("dbo.HistoryChange", new[] { "UserRoleId" });
            DropIndex("dbo.HistoryChange", new[] { "UserId" });
            DropIndex("dbo.Position", new[] { "LeadDisciplineId" });
            DropIndex("dbo.WorkCertificatesWorkCertificates", new[] { "SecondWorkCertificateId" });
            DropIndex("dbo.WorkCertificatesWorkCertificates", new[] { "FirstWorkCertificateId" });
            DropIndex("dbo.HazardRule", "IX_WorkCertificateCategoryIdAreaTypeId");
            DropIndex("dbo.WorkCertificatesHazards", new[] { "HazardId" });
            DropIndex("dbo.WorkCertificatesHazards", new[] { "WorkCertificateId" });
            DropIndex("dbo.Hazard", new[] { "HazardGroupId" });
            DropIndex("dbo.HazardControl", new[] { "HazardId" });
            DropIndex("dbo.WorkCertificatesHazardControls", new[] { "HazardControlId" });
            DropIndex("dbo.WorkCertificatesHazardControls", new[] { "WorkCertificateId" });
            DropIndex("dbo.WorkCertificateAreas", new[] { "AreaId" });
            DropIndex("dbo.WorkCertificateAreas", new[] { "WorkCertificateId" });
            DropIndex("dbo.Location", new[] { "InstallationId" });
            DropIndex("dbo.Area", new[] { "AreaTypeId" });
            DropIndex("dbo.Area", new[] { "LocationId" });
            DropIndex("dbo.User", new[] { "PositionId" });
            DropIndex("dbo.User", new[] { "InstallationId" });
            DropIndex("dbo.User", new[] { "ManagerId" });
            DropIndex("dbo.IsolationPoint", new[] { "IsolationCertificateId" });
            DropIndex("dbo.LeadDiscipline", new[] { "Organization_Id" });
            DropIndex("dbo.ExternalDocument", new[] { "BaseCertificate_Id" });
            DropIndex("dbo.BaseCertificates", new[] { "User_Id" });
            DropIndex("dbo.BaseCertificates", new[] { "LeadDiscipline_Id" });
            DropIndex("dbo.CertificatesUsers", new[] { "UserRoleId" });
            DropIndex("dbo.CertificatesUsers", new[] { "ActionId" });
            DropIndex("dbo.CertificatesUsers", new[] { "UserId" });
            DropIndex("dbo.CertificatesUsers", new[] { "BaseCertificateId" });
            DropTable("dbo.WorkCertificate");
            DropTable("dbo.IsolationCertificate");
            DropTable("dbo.UsersUserRoles");
            DropTable("dbo.UserRolesRolePermissions");
            DropTable("dbo.UsersFavorites");
            DropTable("dbo.UsersAreas");
            DropTable("dbo.WorkCertificatesIsolationCertificates");
            DropTable("dbo.HazardControlsHazardRules");
            DropTable("dbo.IsolationScheme");
            DropTable("dbo.Organization");
            DropTable("dbo.RolePermission");
            DropTable("dbo.UserRole");
            DropTable("dbo.HistoryChange");
            DropTable("dbo.Position");
            DropTable("dbo.WorkCertificatesWorkCertificates");
            DropTable("dbo.WorkCertificateCategories");
            DropTable("dbo.HazardRule");
            DropTable("dbo.WorkCertificatesHazards");
            DropTable("dbo.HazardGroup");
            DropTable("dbo.Hazard");
            DropTable("dbo.HazardControl");
            DropTable("dbo.WorkCertificatesHazardControls");
            DropTable("dbo.WorkCertificateAreas");
            DropTable("dbo.Installation");
            DropTable("dbo.Location");
            DropTable("dbo.AreaTypes");
            DropTable("dbo.Area");
            DropTable("dbo.User");
            DropTable("dbo.IsolationPoint");
            DropTable("dbo.LeadDiscipline");
            DropTable("dbo.ExternalDocument");
            DropTable("dbo.BaseCertificates");
            DropTable("dbo.CertificatesUsers");
            DropTable("dbo.Action");
        }
    }
}
