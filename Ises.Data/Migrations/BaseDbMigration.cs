using System.Data.Entity.Migrations;

namespace Ises.Data.Migrations
{
    public class BaseDbMigration : DbMigration
    {
        public sealed override void Up()
        {
            BeforeUpgade();
            Upgrade();
            AfterUpgrade();
        }

        public sealed override void Down()
        {
            BeforeDowngrade();
            Downgrade();
            AfterDowngrade();
        }

        public virtual void BeforeUpgade()
        {
        }

        public virtual void Upgrade()
        {
        }

        public virtual void AfterUpgrade()
        {
        }

        public virtual void BeforeDowngrade()
        {
        }

        public virtual void Downgrade()
        {
        }

        public virtual void AfterDowngrade()
        {
        }

        public virtual void MigrateDataOnUpgrade()
        {
        }

        public virtual void MigrateDataOnDowngrade()
        {
        }

        public void CreateUnique(string table, string column, string name = null, object anonymousArguments = null)
        {
            CreateUnique(table, new[] { column }, name, anonymousArguments);
        }

        public void CreateUnique(string table, string[] columns, string name = null, object anonymousArguments = null)
        {
            CreateIndex(table, columns, true, name, false, anonymousArguments);
        }

        public void DropUnique(string table, string name, object anonymousArguments = null)
        {
            DropIndex(table, name, anonymousArguments);
        }

        public void DropUnique(string table, string[] columns, object anonymousArguments = null)
        {
            DropIndex(table, columns, anonymousArguments);
        }
    }
}