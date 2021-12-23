namespace Models.Api.Components
{
    public class Ads : ProviderGegevens
    {
        public int display_priority { get; set; }
        public string logo_path { get; set; }
        public int provider_id { get; set; }
        public string provider_name { get; set; }
    }
}