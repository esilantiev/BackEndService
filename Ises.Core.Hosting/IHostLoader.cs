using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ises.Core.Hosting
{
    public interface IHostLoader
    {
        void Start();
        void Stop();
    }
}
