using System;
using Newtonsoft.Json;

namespace Models.Api.Components
{
    public class MovieReleaseDate
    {
        public string certification { get; set; }
        [JsonProperty("iso_639_1")]
        public string iso6391 { get; set; }
        public string note { get; set; }
        [JsonProperty("release_date")]
        public DateTime releaseDate { get; set; }
        public int type { get; set; }
    }
}