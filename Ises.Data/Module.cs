using Autofac;
using Ises.Core.Infrastructure;
using Ises.Core.Utils;
using Ises.Data.DbContexts;
using Ises.Data.MappingSchemes;
using Ises.Data.Repositories;

namespace Ises.Data
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var assembly = GetType().Assembly;
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Implements<IRepository>())
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Implements<IMappingSchemeRegistrator>())
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<IsesDbContext>().As<IDbContext>().InstancePerLifetimeScope();
        }
    }
}
