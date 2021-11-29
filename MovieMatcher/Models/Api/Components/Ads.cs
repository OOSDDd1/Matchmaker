using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieMatcher.Models.Api.Components
{
    public class Ads : ProviderGegevens
    {
        public int display_priority { get; set; }
        public string logo_path { get; set; }
        public int provider_id { get; set; }
        public string provider_name { get; set; }
    }
}
