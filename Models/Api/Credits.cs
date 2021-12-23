using System.Collections.Generic;
using Models.Api.Components;

namespace Models.Api
{
    public class Credits : IRoot
    {
        public int id { get; set; }
        public List<Cast> cast { get; set; }
        public List<Crew> crew { get; set; }
    }
}