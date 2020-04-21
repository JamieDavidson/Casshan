using Newtonsoft.Json;

namespace Casshan.Bindings.Dynamic
{
    internal sealed class MatchMetadataJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public string GameId { get; set; }
    }
}
