using Newtonsoft.Json;

namespace Casshan.Service.Bindings.Dynamic
{
    internal sealed class GameCountBinding
    {
        [JsonProperty(Required = Required.Always)]
        public int TotalGames { get; set; }
    }
}
