using Newtonsoft.Json;

namespace Models.Api.Components
{
    public class ShowContentRatingsResults
    {
        [JsonProperty("iso_3166_1")]
        public string iso31661 { get; set; }
        public string rating { get; set; }
    }
}