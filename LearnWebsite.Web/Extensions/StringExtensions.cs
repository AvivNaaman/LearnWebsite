using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnWebsite.Web.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a string a valid url name
        /// </summary>
        /// <param name="name">The original name, which should be converted to be a url one.</param>
        /// <returns>The url name</returns>
        public static string ConvertToUrlName(this string name)
        {
            return System.Text.RegularExpressions.Regex.Replace(name, @"[^\w\d-_]", "", System.Text.RegularExpressions.RegexOptions.None, TimeSpan.FromSeconds(1.5));
        }
    }
}
