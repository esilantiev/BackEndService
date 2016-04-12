using System.Collections.Generic;
using Ises.Domain.IsolationStates;
using Ises.Domain.IsolationTypes;

namespace Ises.Domain.IsolationMethods
{
    public class IsolationMethod
    {
        public long Id { get; set; }
        public long IsolationTypeId { get; set; }

        public string Name { get; set; }

        public IsolationType IsolationType { get; set; }
        public ICollection<IsolationState> IsolationStates { get; set; }
    }
}
