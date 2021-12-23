using System;

namespace Models.Api.Components
{
    public class MovieReleaseDate
    {
        public string certification { get; set; }
        public string iso_639_1 { get; set; }
        public string note { get; set; }
        public DateTime release_date { get; set; }
        public int type { get; set; }
    }
}