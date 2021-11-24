using System.Collections.Generic;
using MovieMatcher.Models.Api.Components;

namespace MovieMatcher.Models.Api
{
    public class Movie : IRoot
    {
        public int id { get; set; }
        public string title { get; set; }
        public string overview { get; set; }
        public int runtime { get; set; }
        public string release_date { get; set; }
        public bool adult { get; set; }
        public List<Genre> genres { get; set; }

        public bool video { get; set; }
        public string poster_path { get; set; }
        public string backdrop_path { get; set; }
        public Videos videos { get; set; }
        public Images images { get; set; }

        public double vote_average { get; set; }
        public int vote_count { get; set; }
        public double popularity { get; set; }

        public string imdb_id { get; set; }
        public string homepage { get; set; }
        public string tagline { get; set; }
        public int budget { get; set; }
        public int revenue { get; set; }
        public string status { get; set; }

        public BelongsToCollection belongs_to_collection { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public List<ProductionCompany> production_companies { get; set; }
        public List<ProductionCountry> production_countries { get; set; }
        public List<SpokenLanguage> spoken_languages { get; set; }
    }
}