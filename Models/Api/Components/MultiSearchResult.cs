using System.Collections.Generic;
using Newtonsoft.Json;

namespace Models.Api.Components
{
    public class MultiSearchResult : Content
    {
        [JsonProperty("backdrop_path")]
        public string backdropPath { get; set; }
        [JsonProperty("genre_ids")]
        public List<int> genreIds { get; set; }
        public int id { get; set; }
        [JsonProperty("media_type")]
        public string mediaType { get; set; }
        [JsonProperty("original_language")]
        public string originalLanguage { get; set; }
        [JsonProperty("original_title")]
        public string originalTitle { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public string posterPath { get; set; }

        [JsonProperty("profile_path")]
        public string profilePath { get; set; }
        [JsonProperty("release_date")]
        public string releaseDate { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        [JsonProperty("vote_average")]
        public double voteAverage { get; set; }
        [JsonProperty("vote_count")]
        public int voteCount { get; set; }
        [JsonProperty("first_air_date")]
        public string firstAirDate { get; set; }
        public string name { get; set; }
        [JsonProperty("origin_country")]
        public List<string> originCountry { get; set; }
        [JsonProperty("original_name")]
        public string originalName { get; set; }
        [JsonProperty("watch_providers")]
        public Providers watchProviders { get; set; }
        public bool adult { get; set; }
    }
}