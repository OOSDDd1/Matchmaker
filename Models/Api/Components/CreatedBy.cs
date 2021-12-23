using Newtonsoft.Json;

namespace Models.Api.Components
{
    public class CreatedBy
    {
        public int id { get; set; }
        [JsonProperty("credit_id")]
        public string creditId { get; set; }
        public string name { get; set; }
        public int gender { get; set; }
        [JsonProperty("profile_path")]
        public string profilePath { get; set; }
    }
}