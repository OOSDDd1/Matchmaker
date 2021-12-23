using System.Collections.Generic;
using Models.Api.Components;

namespace Models.Api
{
    public class Upcoming : IRoot
    {
        public Dates dates { get; set; }
        public int page { get; set; }
        public List<Movie> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}