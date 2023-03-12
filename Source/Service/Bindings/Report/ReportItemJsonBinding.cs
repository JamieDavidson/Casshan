using Newtonsoft.Json;

namespace Casshan.Bindings.Report
{
    internal sealed class ReportItemJsonBinding
    {
        [JsonProperty(Required = Required.Always)]
        public string SectionName { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int SectionSuspicion { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string[] ReportLines { get; set; }
    }
}