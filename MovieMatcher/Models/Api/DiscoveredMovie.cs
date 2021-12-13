using System.Collections.Generic;

namespace MovieMatcher.Models.Api
{
    public class DiscoveredMovie : IRoot
    {
        public int page { get; set; }
        public List<Movie> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}