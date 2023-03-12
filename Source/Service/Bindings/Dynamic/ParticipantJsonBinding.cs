using Newtonsoft.Json;

namespace Casshan.Service.Bindings.Dynamic
{
    internal sealed class ParticipantJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public int ParticipantId { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Spell1Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Spell2Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public ParticipantsStatsJsonBinding Stats { get; set; }
    }
}