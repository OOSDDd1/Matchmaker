using System.Collections.Generic;
using Models.Api.Components;
using Newtonsoft.Json;

namespace Models.Api
{
    public class Show : IResult, Content
    {
        public int id { get; set; }
        public string name { get; set; }
        public string overview { get; set; }
        public string homepage { get; set; }
        public List<CreatedBy> createdBy { get; set; }
        public List<Network> networks { get; set; }
        [JsonProperty("content_ratings")]
        public ShowContentRatings contentRatings { get; set; }

        public List<Genre> genres { get; set; }
        [JsonProperty("episode_run_time")]
        public List<int> episodeRunTime { get; set; }
        public List<string> languages { get; set; }

        [JsonProperty("first_air_date")]
        public string firstAirDate { get; set; }
        [JsonProperty("in_production")]
        public bool inProduction { get; set; }

        public List<Season> seasons { get; set; }
        [JsonProperty("number_of_seasons")]
        public int numberOfSeasons { get; set; }
        [JsonProperty("number_of_episodes")]
        public int numberOfEpisodes { get; set; }
        [JsonProperty("next_episode_to_air")]
        public Episode nextEpisodeToAir { get; set; }
        [JsonProperty("last_air_date")]
        public string lastAirDate { get; set; }
        [JsonProperty("last_episode_to_air")]
        public Episode lastEpisodeToAir { get; set; }

        [JsonProperty("poster_path")]
        public string posterPath { get; set; }
        [JsonProperty("backdrop_path")]
        public string backdropPath { get; set; }
        public Images images { get; set; }
        public Videos videos { get; set; }

        public string tagline { get; set; }
        public string type { get; set; }
        public double popularity { get; set; }
        public string status { get; set; }
        [JsonProperty("vote_average")]
        public double voteAverage { get; set; }
        [JsonProperty("vote_count")]
        public int voteCount { get; set; }
        [JsonProperty("media_type")]
        public string mediaType { get; set; } = "tv";
        [JsonProperty("origin_country")]
        public List<string> originCountry { get; set; }
        [JsonProperty("original_language")]
        public string originalLanguage { get; set; }
        [JsonProperty("original_name")]
        public string originalName { get; set; }
        [JsonProperty("production_companies")]
        public List<ProductionCompany> productionCompanies { get; set; }
        [JsonProperty("production_countries")]
        public List<ProductionCountry> productionCountries { get; set; }
        [JsonProperty("spoken_languages")]
        public List<SpokenLanguage> spokenLanguages { get; set; }
        public Credits credits { get; set; }

        public Show(string posterPath)
        {
            this.posterPath = posterPath;
        }
    }
}