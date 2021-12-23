using Newtonsoft.Json;

namespace Models.Api
{
    public interface Content : IRoot
    {
        public int id { get; set; }
        [JsonProperty("poster_path")]
        public string posterPath { get; set; }
        [JsonProperty("media_type")]
        public string mediaType { get; set; }
    }
}