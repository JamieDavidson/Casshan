using Newtonsoft.Json;

namespace Casshan.RiotApi.Bindings
{
    internal sealed class GameCountBinding
    {
        [JsonProperty(Required = Required.Always)]
        public int TotalGames { get; set; }
    }
}
