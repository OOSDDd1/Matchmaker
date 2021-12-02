using System.Collections.Generic;
using MovieMatcher.Models.Api.Components;

namespace MovieMatcher.Models.Api
{
    public class ShowContentRatings : IRoot
    {
        public List<ShowContentRatingsResults> results { get; set; }
        public int id { get; set; }
    }
}