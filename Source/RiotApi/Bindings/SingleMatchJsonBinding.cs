using Newtonsoft.Json;

namespace Casshan.RiotApi.Bindings
{
    internal sealed class SingleMatchJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public ParticipantJsonBinding[] Participants { get; set; }

        [JsonProperty(Required = Required.Always)]
        public ParticipantIdentityJsonBinding[] ParticipantIdentities { get; set; }
    }
}
