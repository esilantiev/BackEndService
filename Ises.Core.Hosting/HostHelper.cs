using Autofac;
using Autofac.Core;
using log4net.Config;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Topshelf;

namespace Ises.Core.Hosting
{
    public class HostHelper
    {
        static List<IModule> modulesCache;
        static List<Assembly> assembliesCache;

        public static void RegisterModules(ContainerBuilder builder, params Assembly[] excludedAssemblies)
        {
            var modules = GetModules(excludedAssemblies).ToList();
            foreach (var module in modules)
            {
                builder.RegisterModule(module);
            }
        }

        static IEnumerable<IModule> GetModules(IEnumerable<Assembly> excludedAssemblies)
        {
            return modulesCache ?? (modulesCache = (from t in GetAssemblies(excludedAssemblies).SelectMany(a => a.GetTypes())
                                                    where t.GetInterfaces().Contains(typeof(IModule)) && t.GetConstructor(Type.EmptyTypes) != null
                                                    select Activator.CreateInstance(t) as IModule).ToList());
        }

        static IEnumerable<Assembly> GetAssemblies(IEnumerable<Assembly> excludedAssemblies)
        {
            if (assembliesCache != null) return assembliesCache;
            assembliesCache = new List<Assembly>();
            var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (directoryName != null && Directory.Exists(directoryName))
            {
                var assemblies = Directory.GetFiles(directoryName, "Ises.*.dll").Select(Assembly.LoadFile).ToList();
                assembliesCache = assemblies.Except(excludedAssemblies).ToList();
            }
            return assembliesCache;
        }

        public static void RunHost<T>(HostOptions hostOptions) where T : class, IHostLoader, new()
        {
            var host = HostFactory.New(
                x =>
                {
                    x.Service<T>(
                        s =>
                        {
                            s.ConstructUsing(name => new T());
                            s.WhenStarted(
                                tc =>
                                {
                                    XmlConfigurator.ConfigureAndWatch(new FileInfo(".\\log4net.config"));
                                    tc.Start();
                                });
                            s.WhenStopped(tc => tc.Stop());
                        });

#if !DEBUG
                    var user = Configuration.Host.WinServiceUser;
                    var password = Configuration.Host.WinServicePassword;
                    x.RunAs(user, password);
#endif
                    x.StartAutomatically();
                    x.SetServiceName(hostOptions.ServiceName);
                    x.SetDescription(hostOptions.ServiceDisplayName);
                    x.SetDisplayName(hostOptions.ServiceDisplayName);

                    x.EnableServiceRecovery(s =>
                    {
                        s.RestartService(5);
                        s.SetResetPeriod(5);
                    });
                });
            host.Run();
        }

        public static StartOptions GetStartOptions(string url)
        {
            var startOptions = new StartOptions();
            foreach (string s in url.Split(','))
            {
                startOptions.Urls.Add(s);
            }
            return startOptions;
        }
    }
}
