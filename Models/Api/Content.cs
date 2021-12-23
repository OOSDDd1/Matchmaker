namespace Models.Api
{
    public interface Content : IRoot
    {
        public int id { get; set; }
        public string poster_path { get; set; }
        public string media_type { get; set; }
    }
}