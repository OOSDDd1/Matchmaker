using System.Collections.Generic;
using Newtonsoft.Json;

namespace Models.Api.Components
{
    public class Videos
    {
        public List<VideoList> results { get; set; }

        public class VideoList
        {
            public string id { get; set; }
            public string name { get; set; }
            public string site { get; set; }
            public string key { get; set; }
            [JsonProperty("published_at")]
            public string publishedAt { get; set; }

            public bool official { get; set; }
            public int size { get; set; }
            public string type { get; set; }

            [JsonProperty("iso_639_1")]
            public string iso6391 { get; set; }
            [JsonProperty("iso_3166_1")]
            public string iso31661 { get; set; }
        }
    }
}