using Newtonsoft.Json;

namespace Casshan.Service.Bindings.Static
{
    internal sealed class RuneJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }


        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }
    }
}