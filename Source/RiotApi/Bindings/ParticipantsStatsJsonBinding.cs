using Newtonsoft.Json;

namespace Casshan.RiotApi.Bindings
{
    internal class ParticipantsStatsJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public int Item0 { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Item1 { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Item2 { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Item3 { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Item4 { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Item5 { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Item6 { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Kills { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Deaths { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Assists { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Perk0 { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Perk1 { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Perk2 { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Perk3 { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Perk4 { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Perk5 { get; set; }

        // StatPerks weren't always reported from the API
        // possibly because they didn't exist for long?
        [JsonProperty(Required = Required.Default)]
        public int? StatPerk0 { get; set; }

        [JsonProperty(Required = Required.Default)]
        public int? StatPerk1 { get; set; }

        [JsonProperty(Required = Required.Default)]
        public int? StatPerk2 { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int TotalMinionsKilled { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int VisionWardsBoughtInGame { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int SightWardsBoughtInGame { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int VisionScore { get; set; }

        [JsonProperty(Required = Required.Default)]
        public int? WardsKilled { get; set; }

        [JsonProperty(Required = Required.Default)]
        public int? WardsPlaced { get; set; }
    }
}