using Newtonsoft.Json;

namespace Casshan.RiotApi.Bindings
{
    internal sealed class MatchMetadataJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public string GameId { get; set; }
    }
}
