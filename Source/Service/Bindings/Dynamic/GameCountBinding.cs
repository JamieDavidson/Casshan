using Newtonsoft.Json;

namespace Casshan.Bindings.Dynamic
{
    internal sealed class GameCountBinding
    {
        [JsonProperty(Required = Required.Always)]
        public int TotalGames { get; set; }
    }
}
