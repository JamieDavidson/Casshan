using Newtonsoft.Json;

namespace Casshan.Service.Bindings.Dynamic
{
    internal sealed class AccountJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int SummonerLevel { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string AccountId { get; set; }
    }
}
