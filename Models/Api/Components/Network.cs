using Newtonsoft.Json;

namespace Models.Api.Components
{
    public class Network
    {
        public string name { get; set; }
        public int id { get; set; }
        [JsonProperty("logo_path")]
        public string logoPath { get; set; }
        [JsonProperty("origin_country")]
        public string originCountry { get; set; }
    }
}