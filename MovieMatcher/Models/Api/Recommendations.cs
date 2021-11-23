using System.Collections.Generic;

namespace MovieMatcher.Models.Api
{
    public class Recommendations<T>: IRoot where T: IRoot
    {
        public int page { get; set; }
        public List<T> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}