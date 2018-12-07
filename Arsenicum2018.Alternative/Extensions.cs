using System.Linq;

namespace Arsenicum2018
{
    public static class Extensions
    {
        public static string ReverseString(this string s)
        {
            return new string(s.Reverse().ToArray());
        }

        public static bool IsPalindrom(this string s)
        {
            var ss = s.Replace(" ", "");
            return ss == ss.ReverseString();
        }
    }
}