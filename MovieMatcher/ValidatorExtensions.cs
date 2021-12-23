using System.Text.RegularExpressions;

namespace MovieMatcher
{
    public static class ValidatorExtensions
    {
        public static bool IsValidEmailAddress(this string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,63}$");
            return regex.IsMatch(s);
        }

        /*
         * Regexplanations
         * (?=.*[A-Z])       // should contain at least one upper case
         * (?=.*[a-z])       // should contain at least one lower case
         * (?=.*?[0-9])      // should contain at least one digit
         * (?=.*?[!@#\$&*~]) // should contain at least one Special character
         * .{8,}             // Must be at least 8 characters in length  
         */
        public static bool IsValidPassword(this string s)
        {
            Regex regex = new Regex(@"(?=.*[A-Z])(?=.*[a-z])(?=.*?[0-9])(?=.*?[!@#\$&*~$%^&();,.']).{8,}");
            return regex.IsMatch(s);
        }

        public static bool IsValidDate(this string s)
        {
            Regex regex = new(@"^(?:0?[1-9]|[12]\d|3[01])([\/.-])(?:0?[1-9]|1[012])\1(?:19|20)\d\d$");
            return regex.IsMatch(s);
        }

        public static bool IsValidYoutubeVideoId(this string s)
        {
            Regex regex = new Regex(@"^([a-zA-Z0-9_-]+)&?$");
            return regex.IsMatch(s);
        }
    }
}