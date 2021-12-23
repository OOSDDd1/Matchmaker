using Newtonsoft.Json;

namespace Models.Api.Components
{
    public class Flatrate : IProviderData
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