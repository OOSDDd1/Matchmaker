using System.Collections.Generic;

namespace MovieMatcher.Models.Api.Components
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
            public string published_at { get; set; }

            public bool official { get; set; }
            public int size { get; set; }
            public string type { get; set; }
        
            public string iso_639_1 { get; set; }
            public string iso_3166_1 { get; set; }
        }
    }
}