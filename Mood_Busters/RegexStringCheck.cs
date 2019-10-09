using System.Text.RegularExpressions;

namespace Mood_Busters
{
    static class RegexStringCheck
    {
        private const string pattern = "((Error|Unknown|Happy|Sad|Angry|Confused|Disgusted|Surprised|Calm|Fear) (?:100|[1-9]?[0-9])%)";
        private static Regex rx = new Regex(pattern);

        public static bool checkString(string text)
        {  
            return (rx.IsMatch(text));
        }
    }
}
