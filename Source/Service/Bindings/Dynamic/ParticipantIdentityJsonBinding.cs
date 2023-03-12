using Newtonsoft.Json;

namespace Casshan.Bindings.Dynamic
{
    internal sealed class ParticipantIdentityJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public int ParticipantId { get; set; }

        [JsonProperty(Required = Required.Always)]
        public ParticipantIdentityPlayerJsonBinding Player { get; set; }
    }
}