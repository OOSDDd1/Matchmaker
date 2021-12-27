using Newtonsoft.Json;

namespace Models.Api.Components
{
    public class BelongsToCollection
    {
        public int id { get; set; }
        public string name { get; set; }
        [JsonProperty("poster_path")]
        public string posterPath { get; set; }
        [JsonProperty("backdrop_path")]
        public string backdropPath { get; set; }
    }
}