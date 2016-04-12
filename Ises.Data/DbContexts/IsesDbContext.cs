using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Ises.Core.Common;
using Ises.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Ises.Data.EntityTypeConfigurations;
using Ises.Data.Migrations;
using Ises.Domain.Areas;
using Ises.Domain.CertificatesUsers;
using Ises.Domain.ExternalDocuments;
using Ises.Domain.HazardControls;
using Ises.Domain.HazardGroups;
using Ises.Domain.HazardRules;
using Ises.Domain.Hazards;
using Ises.Domain.HistoryChanges;
using Ises.Domain.Installations;
using Ises.Domain.IsolationCertificates;
using Ises.Domain.IsolationPoints;
using Ises.Domain.LeadDisciplines;
using Ises.Domain.Locations;
using Ises.Domain.Organizations;
using Ises.Domain.Positions;
using Ises.Domain.UserRoles;
using Ises.Domain.Users;
using Ises.Domain.WorkCertificateCategories;
using Ises.Domain.WorkCertificates;
using Ises.Domain.WorkCertificatesHazardControls;
using Ises.Domain.WorkCertificatesHazards;
using Ises.Domain.WorkCertificatesWorkCertificates;
using RefactorThis.GraphDiff;
using TrackerEnabledDbContext;
using TrackerEnabledDbContext.Common.Models;
using Action = Ises.Domain.Actions.Action;

namespace Ises.Data.DbContexts
{
    public class IsesDbContext : TrackerContext, IDbContext
    {
        static bool initialized;
        DbContextTransaction dbTransaction;

        public IsesDbContext()
        {
            Database.Connection.ConnectionString = Core.Common.Configuration.DbConnection.ConnectionString;
            if (initialized) return;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<IsesDbContext, IsesDbConfiguration>());
            initialized = true;
        }


        
        protected IsesDbContext(DbConnection connection)
            : base(connection, true)
        {
            Database.CommandTimeout = 60;
        }
        
        #region DbSets
        public DbSet<Hazard> Hazards { get; set; }
        public DbSet<HazardGroup> HazardGroups { get; set; }
        public DbSet<HazardControl> HazardControls { get; set; }
        public DbSet<Installation> Installations { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<LeadDiscipline> LeadDisciplines { get; set; }
        public DbSet<WorkCertificate> WorkCertificates { get; set; }
        public DbSet<HazardRule> HazardRules { get; set; }
        public DbSet<WorkCertificateHazardControl> WorkCertificatesHazardControls { get; set; }
        public DbSet<WorkCertificateHazard> WorkCertificateHazards { get; set; }
        public DbSet<IsolationCertificate> IsolationCertificates { get; set; }
        public DbSet<IsolationPoint> IsolationPoints { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<CertificateUser> CertificatesUsers { get; set; }
        public DbSet<WorkCertificateWorkCertificate> WorkCertificatesWorkCertificates { get; set; }
        public DbSet<ExternalDocument> ExternalDocuments { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<HistoryChange> HistoryChanges { get; set; }
        public DbSet<WorkCertificateCategory> WorkCertificateCategories { get; set; }
        #endregion DbSets

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ActionTypeConfiguration());
            modelBuilder.Configurations.Add(new AreaTypeConfiguration());
            modelBuilder.Configurations.Add(new CertificateUserTypeConfiguration());
            modelBuilder.Configurations.Add(new ExternalDocumentTypeConfiguration());
            modelBuilder.Configurations.Add(new HazardControlTypeConfiguration());
            modelBuilder.Configurations.Add(new HazardGroupTypeConfiguration());
            modelBuilder.Configurations.Add(new HazardRuleTypeConfiguration());
            modelBuilder.Configurations.Add(new HazardTypeConfiguration());
            modelBuilder.Configurations.Add(new HistoryChangeTypeConfiguration());
            modelBuilder.Configurations.Add(new InstallationTypeConfiguration());
            modelBuilder.Configurations.Add(new IsolationCertificateTypeConfiguration());
            modelBuilder.Configurations.Add(new IsolationPointTypeConfiguration());
            modelBuilder.Configurations.Add(new LeadDisciplineTypeConfiguration());
            modelBuilder.Configurations.Add(new LocationTypeConfiguration());
            modelBuilder.Configurations.Add(new OrganizationTypeConfiguration());
            modelBuilder.Configurations.Add(new PositionTypeConfiguration());
            modelBuilder.Configurations.Add(new RolePermissionTypeConfiguration());
            modelBuilder.Configurations.Add(new UserRoleTypeConfiguration());
            modelBuilder.Configurations.Add(new UserTypeConfiguration());
            modelBuilder.Configurations.Add(new WorkCertificateAreaTypeConfiguration());
            modelBuilder.Configurations.Add(new WorkCertificateHazardTypeConfiguration());
            modelBuilder.Configurations.Add(new WorkCertificateHazardControlTypeConfiguration());
            modelBuilder.Configurations.Add(new WorkCertificateTypeConfiguration());
            modelBuilder.Configurations.Add(new WorkCertificateWorkCertificateTypeConfiguration());
            modelBuilder.Configurations.Add(new AreaTypeTypeConfiguration());
            modelBuilder.Configurations.Add(new WorkCertificateCategoryTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public void SetProxyCreationEnabled(bool flag)
        {
            Configuration.ProxyCreationEnabled = flag;
        }

        public IQueryable<T> DbSet<T>() where T : class
        {
            return Set<T>();
        }

        public IQueryable<AuditLog> GetLogsFor<T>() where T : class
        {
            return GetLogs<T>();
        }

        public T Add<T>(T entity, string mappingScheme) where T : class
        {
            AttachToTransaction();
            var updated = this.UpdateGraph(entity, mappingScheme); 
            return updated;
        }

        public T Update<T>(T entity, string mappingScheme) where T : class
        {
            AttachToTransaction();
            var updated = this.UpdateGraph(entity, mappingScheme);
            return updated;
        }

        public T Delete<T>(T entity) where T : class
        {
            AttachToTransaction();
            return Set<T>().Remove(entity);
        }

        public async Task<int> SaveAsync()
        {
            int result;
            try
            {
                result = await SaveChangesAsync();

                if (dbTransaction != null)
                    dbTransaction.Commit();
            }
            catch (Exception ex)
            {
                ApplicationContext.Logger.ErrorFormat("Got Error when saving changes to database: {0}", ex);
                if (dbTransaction != null)
                    dbTransaction.Rollback();
                throw;
            }
            finally
            {
                if (dbTransaction != null)
                {
                    dbTransaction.Dispose();
                    dbTransaction = null;
                }
            }
            return result;
        }

        public async Task<List<T>> SqlQueryAsync<T>(string sql, params KeyValuePair<string, object>[] parameters)
        {
            if (Database.Connection.State != ConnectionState.Open) Database.Connection.Open();
            var paramList = parameters.Select(x => new SqlParameter(x.Key, x.Value)).Cast<object>().ToArray();
            var query = await Database.SqlQuery<T>(sql, paramList).ToListAsync();
            return query;
        }

        void AttachToTransaction()
        {
            if (dbTransaction != null) return;
            if (Database.Connection.State != ConnectionState.Open)
                Database.Connection.Open();
            dbTransaction = Database.BeginTransaction();
        }
    }
}
