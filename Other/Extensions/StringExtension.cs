using System;
using System.Linq;

namespace Test_Task_for_GeeksForLess.Other.Extensions
{
    public static class StringExtension
    {
        public static String RemoveWhiteSpace(this String input)
        {
            return new String(input.ToCharArray()
            .Where(c => !Char.IsWhiteSpace(c))
            .ToArray());
        }
    }
}
