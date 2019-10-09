using System.Text.RegularExpressions;

namespace Mood_Busters
{
    static class RegexStringCheck
    {
        private const string pattern = "((Error|Unknown|Happy|Sad|Angry|Confused|Disgusted|Surprised|Calm|Fear) (?:100|[1-9]?[0-9])%)";
        public static bool checkString(string text)
        {  
            return (new Regex(pattern)).IsMatch(text);
        }
    }
}
