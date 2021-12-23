using System.Collections.Generic;

namespace Models.Api.Components
{
    public class MovieReleaseDateResult
    {
        public string iso_3166_1 { get; set; }
        public List<MovieReleaseDate> release_dates { get; set; }
    }
}