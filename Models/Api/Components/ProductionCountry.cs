using Newtonsoft.Json;

namespace Models.Api.Components
{
    public class ProductionCountry
    {
        [JsonProperty("iso_3166_1")]
        public string iso31661 { get; set; }
        public string name { get; set; }
    }
}