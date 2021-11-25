using System.Collections.Generic;
using MovieMatcher.Models.Api.Components;

namespace MovieMatcher.Models.Api
{
    public class Providers : IRoot
    {
        public int id { get; set; }
        public Dictionary<string, Provider> results { get; set; }
    }

    public class Provider
    {
        public string link { get; set; }
        public List<Flatrate> flatrate { get; set; }
        public List<Rent> rent { get; set; }
        public List<Buy> buy { get; set; }
    }
}