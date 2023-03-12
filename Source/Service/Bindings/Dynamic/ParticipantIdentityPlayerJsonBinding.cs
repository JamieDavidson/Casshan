using Newtonsoft.Json;

namespace Casshan.Service.Bindings.Dynamic
{
    internal sealed class ParticipantIdentityPlayerJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public string AccountId { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string SummonerName { get; set; }

        // SummonerId is absent for AI participants, so Required.Default allows us
        // to accept a binding without the field and set SummonerId to null
        [JsonProperty(Required = Required.Default)]
        public string SummonerId { get; set; }
    }
}