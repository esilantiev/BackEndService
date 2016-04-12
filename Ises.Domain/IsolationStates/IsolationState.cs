using Ises.Domain.IsolationMethods;

namespace Ises.Domain.IsolationStates
{
    public class IsolationState
    {
        public long Id { get; set; }
        public long IsolationMethodId { get; set; }
        public long? IsolationStateId { get; set; }

        public string Name { get; set; }

        public IsolationMethod IsolationMethod { get; set; }
        public IsolationState OppositeState { get; set; }
    }
}
