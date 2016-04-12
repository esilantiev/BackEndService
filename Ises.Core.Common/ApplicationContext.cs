using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Ises.Core.Common
{
    public class ApplicationContext
    {
        static readonly ILog LoggerInstance = LogManager.GetLogger("Logger");
        public static ILog Logger
        {
            get { return LoggerInstance; }
        }

        public static string HostName { get; set; }
    }
}
