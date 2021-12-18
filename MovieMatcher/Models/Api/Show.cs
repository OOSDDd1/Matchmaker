using System.Collections.Generic;
using MovieMatcher.Models.Api.Components;

namespace MovieMatcher.Models.Api
{
    public class Show : IResult, Content
    {
        public int id { get; set; }
        public string name { get; set; }
        public string overview { get; set; }
        public string homepage { get; set; }
        public List<CreatedBy> created_by { get; set; }
        public List<Network> networks { get; set; }
        public ShowContentRatings content_ratings { get; set; }

        public List<Genre> genres { get; set; }
        public List<int> episode_run_time { get; set; }
        public List<string> languages { get; set; }

        public string first_air_date { get; set; }
        public bool in_production { get; set; }

        public List<Season> seasons { get; set; }
        public int number_of_seasons { get; set; }
        public int number_of_episodes { get; set; }
        public Episode next_episode_to_air { get; set; }
        public string last_air_date { get; set; }
        public Episode last_episode_to_air { get; set; }

        public string poster_path { get; set; }
        public string backdrop_path { get; set; }
        public Images images { get; set; }
        public Videos videos { get; set; }

        public string tagline { get; set; }
        public string type { get; set; }
        public double popularity { get; set; }
        public string status { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
        public string media_type { get; set; } = "tv";
        public List<string> origin_country { get; set; }
        public string original_language { get; set; }
        public string original_name { get; set; }
        public List<ProductionCompany> production_companies { get; set; }
        public List<ProductionCountry> production_countries { get; set; }
        public List<SpokenLanguage> spoken_languages { get; set; }
        public Credits credits { get; set; }

        public Show(string poster_path)
        {
            this.poster_path = poster_path;
        }
    }
}