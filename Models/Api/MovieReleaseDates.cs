using System.Collections.Generic;
using Models.Api.Components;

namespace Models.Api
{
    public class MovieReleaseDates : IRoot
    {
        public int id { get; set; }
        public List<MovieReleaseDateResult> results { get; set; }
    }
}