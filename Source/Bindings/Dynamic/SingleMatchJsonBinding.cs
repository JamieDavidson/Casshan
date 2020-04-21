using Newtonsoft.Json;

namespace Casshan.Bindings.Dynamic
{
    internal sealed class SingleMatchJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public ParticipantJsonBinding[] Participants { get; set; }

        [JsonProperty(Required = Required.Always)]
        public ParticipantIdentityJsonBinding[] ParticipantIdentities { get; set; }
    }
}
