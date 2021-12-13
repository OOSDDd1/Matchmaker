using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieMatcher.Models.Api
{
    interface IResult
    {
        public int id { get; set; }
        public string media_type { get; set; }
    }
}