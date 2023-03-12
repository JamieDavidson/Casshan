using System.Collections.Generic;
using Newtonsoft.Json;

namespace Casshan.Bindings.Static
{
    internal sealed class ItemsJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public Dictionary<int, ItemJsonBinding> Data { get; set; }
    }

    internal sealed class ItemJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }
    }
}
