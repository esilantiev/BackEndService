using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ises.Core.Common;
using Ises.Core.Hosting;
using log4net.Config;
using Microsoft.Owin.Hosting;

namespace Ises.BackOffice.Host
{
    public class Loader : IHostLoader
    {
        IDisposable app;

        public Loader()
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(".\\log4net.config"));
        }

        public void Start()
        {
            ApplicationContext.Logger.Warn(".......................................");
            var url = Configuration.Host.BackOfficeApiBaseAddress;
            ApplicationContext.Logger.WarnFormat("Starting Ises BackOffice API on '{0}' ...", url);
            app = WebApp.Start<StartUp>(HostHelper.GetStartOptions(url));
            ApplicationContext.Logger.Warn("Ises BackOffice API loaded.");
        }

        public void Stop()
        {
            ApplicationContext.Logger.Warn(".......................................");
            ApplicationContext.Logger.Warn("Stopping Ises BackOffice API ...");
            app.Dispose();
            ApplicationContext.Logger.Warn("Service is shut down.");
        }
    }
}
