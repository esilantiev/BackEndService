using System.Data.Entity.Migrations;
using Ises.Data.DbContexts;

namespace Ises.Data.Migrations
{
    public sealed class IsesDbConfiguration : DbMigrationsConfiguration<IsesDbContext>
    {
        public IsesDbConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            CodeGenerator = new ExtendedCodeGenerator();
        }

        protected override void Seed(IsesDbContext context)
        {
            base.Seed(context);
            DatabaseLoader.Seed(context);
        }
    }
}
