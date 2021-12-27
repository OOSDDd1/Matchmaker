using Newtonsoft.Json;

namespace Models.Api.Components
{
    public class ProductionCompany
    {
        public int id { get; set; }
        public string name { get; set; }
        [JsonProperty("logo_path")]
        public string logoPath { get; set; }
        [JsonProperty("origin_country")]
        public string originCountry { get; set; }
    }
}