using Newtonsoft.Json;

namespace Models.Api.Components
{
    public class Season
    {
        [JsonProperty("air_date")]
        public string airDate { get; set; }
        [JsonProperty("episode_count")]
        public int episodeCount { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string overview { get; set; }
        [JsonProperty("poster_path")]
        public string posterPath { get; set; }
        [JsonProperty("season_number")]
        public int seasonNumber { get; set; }
    }
}