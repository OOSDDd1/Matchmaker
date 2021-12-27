using System.Collections.Generic;
using Models.Api.Components;
using Newtonsoft.Json;

namespace Models.Api
{
    public class Movie : IResult, Content
    {
        public int id { get; set; }
        public string title { get; set; }
        public string overview { get; set; }
        public int runtime { get; set; }
        [JsonProperty("release_date")]
        public string releaseDate { get; set; }
        public bool adult { get; set; }
        public List<Genre> genres { get; set; }
        [JsonProperty("release_dates")]
        public MovieReleaseDates releaseDates { get; set; }
        [JsonProperty("genre_ids")]
        public List<int> genreIds { get; set; }

        public bool video { get; set; }
        [JsonProperty("poster_path")]
        public string posterPath { get; set; }
        [JsonProperty("backdrop_path")]
        public string backdropPath { get; set; }
        public Videos videos { get; set; }
        public Images images { get; set; }

        [JsonProperty("vote_average")]
        public double voteAverage { get; set; }
        [JsonProperty("vote_count")]
        public int voteCount { get; set; }
        public double popularity { get; set; }

        [JsonProperty("imdb_id")]
        public string imdbId { get; set; }
        public string homepage { get; set; }
        public string tagline { get; set; }
        public int budget { get; set; }
        public ulong revenue { get; set; }
        public string status { get; set; }
        [JsonProperty("media_type")]
        public string mediaType { get; set; } = "movie";
        [JsonProperty("belongs_to_collection")]
        public BelongsToCollection belongsToCollection { get; set; }
        [JsonProperty("original_language")]
        public string originalLanguage { get; set; }
        [JsonProperty("original_title")]
        public string originalTitle { get; set; }
        [JsonProperty("production_companies")]
        public List<ProductionCompany> productionCompanies { get; set; }
        [JsonProperty("production_countries")]
        public List<ProductionCountry> productionCountries { get; set; }
        [JsonProperty("spoken_languages")]
        public List<SpokenLanguage> spokenLanguages { get; set; }
        public Credits credits { get; set; }

        public Movie(string posterPath)
        {
            this.posterPath = posterPath;
        }
    }
}