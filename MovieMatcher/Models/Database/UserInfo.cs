using Microsoft.VisualBasic;

namespace MovieMatcher.Models.Database
{
    class UserInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string birth_year { get; set; }
    }
}
