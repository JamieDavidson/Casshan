using Newtonsoft.Json;

namespace Casshan.RiotApi.Bindings
{
    internal sealed class MatchesJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public MatchMetadataJsonBinding[] Matches { get; set; }
    }
}
