using System.Collections.Generic;
using Newtonsoft.Json;

namespace Models.Api
{
    public class DiscoveredMovie : IRoot
    {
        public int page { get; set; }
        public List<Movie> results { get; set; }
        [JsonProperty("total_pages")]
        public int totalPages { get; set; }
        [JsonProperty("total_results")]
        public int totalResults { get; set; }
    }
}