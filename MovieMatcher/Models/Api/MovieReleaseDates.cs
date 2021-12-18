using System.Collections.Generic;
using MovieMatcher.Models.Api.Components;

namespace MovieMatcher.Models.Api
{
    public class MovieReleaseDates : IRoot
    {
        public int id { get; set; }
        public List<MovieReleaseDateResult> results { get; set; }
    }
}