using System;
using Newtonsoft.Json;

namespace Ises.Core.Api.Help.ModelDescriptions
{
    /// <summary>
    /// Describes a type model.
    /// </summary>
    public abstract class ModelDescription
    {
        public string Documentation { get; set; }

        [JsonIgnore]
        public Type ModelType { get; set; }

        public string Name { get; set; }
    }
}