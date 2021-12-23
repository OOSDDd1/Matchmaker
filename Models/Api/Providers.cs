using System.Collections.Generic;
using Models.Api.Components;

namespace Models.Api
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
        public List<Ads> ads { get; set; }
        public List<Rent> rent { get; set; }
        public List<Buy> buy { get; set; }
    }

    public interface ProviderGegevens
    {
        public int display_priority { get; set; }
        public string logo_path { get; set; }
        public int provider_id { get; set; }
        public string provider_name { get; set; }
    }
}