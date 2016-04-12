using System.Collections.Generic;
using Ises.Domain.IsolationMethods;

namespace Ises.Domain.IsolationTypes
{
    public class IsolationType
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<IsolationMethod> IsolationMethods { get; set; }
    }
}
