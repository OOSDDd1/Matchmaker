using System.Collections.Generic;
using Models.Api.Components;

namespace Models.Api
{
    public class MultiSearch : IRoot
    {
        public int page { get; set; }
        public List<MultiSearchResult> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}