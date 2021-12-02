using MovieMatcher;
using NUnit.Framework;

namespace Tests
{
    public class ValidatorExtensionsTests
    {
        [TestCase("email@example.com")]
        [TestCase("firstname.lastname@example.com")]
        [TestCase("email@subdomain.example.com")]
        [TestCase("email@123.123.123.123")]
        [TestCase("1234567890@example.com")]
        [TestCase("email@example-one.com")]
        [TestCase("_______@example.com")]
        [TestCase("email@example.name")]
        [TestCase("email@example.museum")]
        [TestCase("email@example.co.jp")]
        public void IsValidEmailAddress_ShouldReturnTrueWithValidEmail(string s)
        {
            Assert.True(ValidatorExtensions.IsValidEmailAddress(s));
        }
        
        [TestCase("plainaddress")]
        [TestCase("#@%^%#$@#$@#.com")]
        [TestCase("@example.com")]
        [TestCase("Joe Smith <email@example.com>")]
        [TestCase("email.example.com")]
        [TestCase("email@example@example.com")]
        [TestCase("email@example.com (Joe Smith)")]
        [TestCase("email@example")]
        [TestCase("email@example..com")]
        [TestCase("”(),:;<>[\\]@example.com")]
        [TestCase("just”not”right@example.com")]
        [TestCase("this\\ is\"really\"not\allowed@example.com")]
        public void IsValidEmailAddress_ShouldReturnTrueWithInvalidEmail(string s)
        {
            Assert.False(ValidatorExtensions.IsValidEmailAddress(s));
        }

        [TestCase("AAaa11!!")]
        [TestCase("TestTest123!")]
        [TestCase("DionPotkampGamingNL42069!@#")]
        [TestCase("YEETyeet!@4")]
        [TestCase("1010101010aA!")]
        [TestCase("!@#$%^&*()_+aA69")]
        [TestCase("^yYaAbB61")]
        public void IsValidPassword_ShouldReturnTrueWithValidPassword(string s)
        {
            Assert.True(ValidatorExtensions.IsValidPassword(s));
        }
        
        [TestCase("AAaa1!!")]
        [TestCase("TestTest!")]
        [TestCase("DionPotkampGamingNL42069")]
        [TestCase("YEETyeet1")]
        [TestCase("1010101010aA")]
        [TestCase("!@#$%^&*()_+A69")]
        [TestCase("^yYaAbBasdxcsd")]
        [TestCase("               ")]
        public void IsValidPassword_ShouldReturnTrueWithInvalidPassword(string s)
        {
            Assert.False(ValidatorExtensions.IsValidPassword(s));
        }

        [TestCase("02/12/1984")]
        [TestCase("02-12-1984")]
        [TestCase("03/10/1999")]
        [TestCase("3-10-1999")]
        [TestCase("7/1/1984")]
        [TestCase("02/12/2021")]
        [TestCase("2-1-2021")]
        [TestCase("2/1/2021")]
        public void IsValidDate_ShouldReturnTrueWithValidDate(string s)
        {
            Assert.True(ValidatorExtensions.IsValidDate(s));
        }
        
        [TestCase("32/11/1987")]
        [TestCase("32-11-1987")]
        [TestCase("3-11/1987")]
        [TestCase("13/20/1984")]
        [TestCase("13-20-1984")]
        [TestCase("1984/11/12")]
        [TestCase("1984-11-12")]
        [TestCase("15/1984/15")]
        [TestCase("15-1984-15")]
        [TestCase("15/18-198")]
        [TestCase("15/18/198")]
        [TestCase("15-18-198")]
        public void IsValidDate_ShouldReturnTrueWithInvalidDate(string s)
        {
            Assert.False(ValidatorExtensions.IsValidDate(s));
        }
    }
}