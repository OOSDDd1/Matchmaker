using System.Collections.Generic;
using Newtonsoft.Json;

namespace Models.Api
{
    public class Recommendations<T> : IRoot where T : IRoot
    {
        public int page { get; set; }
        public List<T> results { get; set; }
        [JsonProperty("total_pages")]
        public int totalPages { get; set; }
        [JsonProperty("total_results")]
        public int totalResults { get; set; }
    }
}