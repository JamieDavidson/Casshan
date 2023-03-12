using System.Collections.Generic;
using Newtonsoft.Json;

namespace Casshan.Bindings.Static
{
    internal sealed class SummonersJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public Dictionary<string, SummonerJsonBinding> Data { get; set; }
    }

    internal sealed class SummonerJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Key { get; set; }
    }
}
