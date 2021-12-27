using System.Collections.Generic;
using Newtonsoft.Json;

namespace Models.Api.Components
{
    public class MovieReleaseDateResult
    {
        [JsonProperty("iso_3166_1")]
        public string iso31661 { get; set; }
        [JsonProperty("release_dates")]
        public List<MovieReleaseDate> releaseDates { get; set; }
    }
}