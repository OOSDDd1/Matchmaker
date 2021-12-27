using System.Collections.Generic;
using Models.Api.Components;
using Newtonsoft.Json;

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

    public interface IProviderData
    {
        [JsonProperty("display_priority")]
        public int displayPriority { get; set; }
        [JsonProperty("logo_path")]
        public string logoPath { get; set; }
        [JsonProperty("provider_id")]
        public int providerId { get; set; }
        [JsonProperty("provider_name")]
        public string providerName { get; set; }
    }
}