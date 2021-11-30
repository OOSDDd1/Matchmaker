using Microsoft.VisualBasic;
using System;

namespace MovieMatcher.Models.Database
{
    public static class UserInfo
    { 
        public static int Id { get; set; }
        public static string? Username { get; set; }
        public static string? Email { get; set; }
        public static string? Password { get; set; }
        public static DateTime BirthYear { get; set; }
    }
}
