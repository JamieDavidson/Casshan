using Newtonsoft.Json;

namespace Casshan.Bindings.Dynamic
{
    internal sealed class MatchesJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public MatchMetadataJsonBinding[] Matches { get; set; }
    }
}
