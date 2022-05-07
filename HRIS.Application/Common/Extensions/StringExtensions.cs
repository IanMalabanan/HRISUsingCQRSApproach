using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HRIS.Application.Common.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex sWhitespace = new Regex(@"\s+");
        public static string RemoveWhiteSpaces(this string str)
        {
            return sWhitespace.Replace(str, string.Empty);
        }
    }
}
