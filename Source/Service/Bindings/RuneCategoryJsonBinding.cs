using Newtonsoft.Json;

namespace Casshan.Service.Bindings.Static
{
    internal sealed class RuneCategoryJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public RuneSlotJsonBinding[] Slots { get; set; }
    }
}
