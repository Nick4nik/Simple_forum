using System;
using System.Linq;

namespace Simple_forum.Other.Extensions
{
    public static class StringExtension
    {
        public static String RemoveWhiteSpace(this String input)
        {
            char charToTrim = ' ';
            string result = input.Trim(charToTrim);
            return result;
        }
    }
}
