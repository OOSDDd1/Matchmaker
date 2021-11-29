using System.Collections.Generic;

namespace MovieMatcher.Models.Api.Components
{
    public class Images
    {
        public List<object> backdrops { get; set; }
        public List<object> logos { get; set; }
        public List<object> posters { get; set; }
    }
}