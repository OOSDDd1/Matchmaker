using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieMatcher.Models.Api.Components;

namespace MovieMatcher.Models.Api
{
    public class MultiSearch : IRoot
    {
        public int page { get; set; }
        public List<MultiSearchResult> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}
