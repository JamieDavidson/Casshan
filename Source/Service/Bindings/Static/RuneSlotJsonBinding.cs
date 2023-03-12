using Newtonsoft.Json;

namespace Casshan.Bindings.Static
{
    internal sealed class RuneSlotJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public RuneJsonBinding[] Runes { get; set; }
    }
}