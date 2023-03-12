using Newtonsoft.Json;

namespace Casshan.Service.Bindings.Static
{
    internal sealed class RuneSlotJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public RuneJsonBinding[] Runes { get; set; }
    }
}