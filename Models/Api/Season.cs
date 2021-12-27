using System.Collections.Generic;
using Models.Api.Components;
using Newtonsoft.Json;

namespace Models.Api
{
    public class Season : IRoot
    {
        public string _id { get; set; }
        [JsonProperty("air_date")]
        public string airDate { get; set; }
        public List<Episode> episodes { get; set; }
        public string name { get; set; }
        public string overview { get; set; }
        public int id { get; set; }
        [JsonProperty("poster_path")]
        public string posterPath { get; set; }
        [JsonProperty("season_number")]
        public int seasonNumber { get; set; }
        public Videos videos { get; set; }
        public Images images { get; set; }
    }
}