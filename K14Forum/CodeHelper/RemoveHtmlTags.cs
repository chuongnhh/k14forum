using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace K14Forum.CodeHelper
{
    public static class RemoveHtmlTags
    {
        /// <summary>
        /// http://jou.vn/article/loai-bo-html-tags-trong-chuoi-bang-c
        /// </summary>
        /// <param name="htmlString"></param>
        /// <returns></returns>
        public static string RemoveHtmlTagsUsingRegex(this string htmlString)
        {
            var result = Regex.Replace(htmlString, "<.*?>", string.Empty);
            return result;
        }
        static readonly Regex HtmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        public static string RemoveHtmlTagsUsingCompiledRegex(this string htmlString)
        {
            var result = HtmlRegex.Replace(htmlString, string.Empty);
            return result;
        }

        public static string RemoveHtmlTagsUsingCharArray(this string htmlString)
        {
            var array = new char[htmlString.Length];
            var arrayIndex = 0;
            var inside = false;

            foreach (var @let in htmlString)
            {
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (inside) continue;
                array[arrayIndex] = let;
                arrayIndex++;
            }
            return new string(array, 0, arrayIndex);
        }

    }
}