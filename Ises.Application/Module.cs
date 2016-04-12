using Autofac;
using Ises.Application.Managers;
using Ises.Core.Utils;

namespace Ises.Application
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var assembly = GetType().Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Implements<IManager>())
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            
        }

        
    }
}
