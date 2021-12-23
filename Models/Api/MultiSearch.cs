using System.Collections.Generic;
using Models.Api.Components;
using Newtonsoft.Json;

namespace Models.Api
{
    public class MultiSearch : IRoot
    {
        public int page { get; set; }
        public List<MultiSearchResult> results { get; set; }
        [JsonProperty("total_pages")]
        public int totalPages { get; set; }
        [JsonProperty("total_results")]
        public int totalResults { get; set; }
    }
}