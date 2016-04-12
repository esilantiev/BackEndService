using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ises.Core.Hosting;

namespace Ises.BackOffice.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var hostOptions = new HostOptions
            {
                ServiceName = "IsesBackOfficeService",
                ServiceDisplayName = "Ises BackOffice Service"
            };
            HostHelper.RunHost<Loader>(hostOptions);
        }
    }
}
