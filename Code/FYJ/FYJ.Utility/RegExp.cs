using System.Text.RegularExpressions;

namespace FYJ.Utility
{
    public class RegExp
    {
        private static bool IsMatch(string pattern, string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// Check mail address
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEmail(string input)
        {
            string pattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            return IsMatch(pattern, input);
        }
    }
}
