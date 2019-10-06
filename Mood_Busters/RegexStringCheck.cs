﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mood_Busters
{
    class RegexStringCheck : IRegex
    {
        private const string pattern = "((Error|Unknown|Happy|Sad|Angry|Confused|Disgusted|Surprised|Calm|Fear) (?:100|[1-9]?[0-9])%)";

        public bool checkString(string text)
        {
            Regex rx = new Regex(pattern);
            return rx.IsMatch(text);
        }
    }
}
