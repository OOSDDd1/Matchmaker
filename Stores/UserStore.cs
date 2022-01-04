using System;

namespace Stores
{
    public static class UserStore
    {
        public static int? id { get; set; }
        public static string username { get; set; }
        public static string email { get; set; }
        public static string password { get; set; }
        public static DateTime? birthYear { get; set; }
        public static bool? adult { get; set; }
        public static bool? provider { get; set; }

        public static void Clear()
        {
            id = null;
            username = null;
            email = null;
            password = null;
            birthYear = null;
            adult = null;
            provider = null;
        }
    }
}