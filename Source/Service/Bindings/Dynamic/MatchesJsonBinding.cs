using Newtonsoft.Json;

namespace Casshan.Service.Bindings.Dynamic
{
    internal sealed class MatchesJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public MatchMetadataJsonBinding[] Matches { get; set; }
    }
}
