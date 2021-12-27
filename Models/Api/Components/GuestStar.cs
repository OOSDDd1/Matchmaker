using Newtonsoft.Json;

namespace Models.Api.Components
{
    public class GuestStar
    {
        public string character { get; set; }
        [JsonProperty("credit_id")]
        public string creditId { get; set; }
        public int order { get; set; }
        public bool adult { get; set; }
        public int gender { get; set; }
        public int id { get; set; }
        [JsonProperty("known_for_department")]
        public string knownForDepartment { get; set; }
        public string name { get; set; }
        [JsonProperty("original_name")]
        public string originalName { get; set; }
        public double popularity { get; set; }
        [JsonProperty("profile_path")]
        public string profilePath { get; set; }
    }
}