using System.Collections.Generic;
using Models.Api.Components;
using Newtonsoft.Json;

namespace Models.Api
{
    public class Episode : IRoot

    {
        public int id { get; set; }
        [JsonProperty("season_number")]
        public int seasonNumber { get; set; }
        [JsonProperty("episode_number")]
        public int episodeNumber { get; set; }

        public string name { get; set; }
        public string overview { get; set; }
        [JsonProperty("air_date")]
        public string airDate { get; set; }

        [JsonProperty("still_path")]
        public string stillPath { get; set; }
        public Videos videos { get; set; }
        public Images images { get; set; }

        [JsonProperty("production_code")]
        public string productionCode { get; set; }
        public List<Crew> crew { get; set; }
        [JsonProperty("guest_stars")]
        public List<GuestStar> guestStars { get; set; }

        [JsonProperty("vote_average")]
        public double voteAverage { get; set; }
        [JsonProperty("vote_count")]
        public int voteCount { get; set; }
    }
}