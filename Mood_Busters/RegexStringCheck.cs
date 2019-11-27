using System.Text.RegularExpressions;

namespace AndroMooda3
{
    static class RegexStringCheck
    {
        private const string pattern = "((Error|Unknown|Happy|Sad|Angry|Confused|Disgusted|Surprised|Calm|Fear) (?:100|[1-9]?[0-9])%)";
        private static Regex rx = new Regex(pattern);

        public delegate bool ReturnValue(bool value); //Anonymous method
        

        public static bool checkString(string text)
        {
            ReturnValue rV = delegate (bool value) //Anonymous method takes in bool value, and returns the same bool value
            {
                value = rx.IsMatch(text);
                return value;
            };
           
            return rV(rx.IsMatch(text));
        }

    }
}
