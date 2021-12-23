using Newtonsoft.Json;

namespace Models.Api.Components
{
    public class SpokenLanguage
    {
        public string name { get; set; }
        [JsonProperty("iso_639_1")]
        public string iso6391 { get; set; }
        [JsonProperty("english_name")]
        public string englishName { get; set; }
    }
}