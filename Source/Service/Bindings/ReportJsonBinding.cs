using System.Collections.Generic;
using Newtonsoft.Json;

namespace Casshan.Service.Bindings.Report
{
    internal sealed class ReportJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public string SummonerName { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string AccountId { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int SuspicionRating { get; set; }

        [JsonProperty(Required = Required.Always)]
        public List<ReportItemJsonBinding> ReportItems { get; set; }
    }
}
