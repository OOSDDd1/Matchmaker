namespace MovieMatcher.Models.Api
{
    public class Message: IRoot
    {
        public bool success { get; set; }
        public int status_code { get; set; }
        public string status_message { get; set; }
    }
}