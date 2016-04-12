using System;

namespace Ises.Core.Api.Help.ModelDescriptions
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class DocSampleAttribute : Attribute
    {
        public DocSampleAttribute(object value)
        {
            Value = value;
        }

        public object Value { get; private set; }
    }
}