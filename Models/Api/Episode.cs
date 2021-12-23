using System.Collections.Generic;
using Models.Api.Components;

namespace Models.Api
{
    public class Episode : IRoot

    {
        public int id { get; set; }
        public int season_number { get; set; }
        public int episode_number { get; set; }

        public string name { get; set; }
        public string overview { get; set; }
        public string air_date { get; set; }

        public string still_path { get; set; }
        public Videos videos { get; set; }
        public Images images { get; set; }

        public string production_code { get; set; }
        public List<Crew> crew { get; set; }
        public List<GuestStar> guest_stars { get; set; }

        public double vote_average { get; set; }
        public int vote_count { get; set; }
    }
}