using Newtonsoft.Json;

namespace Models.Api
{
    public class Message : IRoot
    {
        public bool success { get; set; }
        [JsonProperty("status_code")]
        public int statusCode { get; set; }
        [JsonProperty("status_message")]
        public string statusMessage { get; set; }
    }
}