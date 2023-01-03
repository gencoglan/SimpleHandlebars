using BasicHandleBars.Segment.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BasicHandleBars.Segment
{
    public static class DataExtension
    {
        static Regex htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        public static string RemoveHTMLTagsCompiled(string html)
        {
            return htmlRegex.Replace(html, string.Empty);
        }
        public static object getValue(string prop, IHasData segment)
        {
            if (prop.StartsWith("{{"))
            {
                prop = prop.Remove(0, 2);
            }
            if (prop.EndsWith("}}"))
            {
                prop = prop.Remove(prop.Length - 2, 2);
            }
            var IsSafe = false;
            if (prop.StartsWith("{"))
            {
                prop = prop.Remove(0, 1);
                prop = prop.Remove(prop.Length - 1, 1);
                IsSafe = true;
            }
            object value = null;
            if (segment.Data != null)
            {
                if (prop == "this" || prop == "{{this}}" || prop == "{{{this}}}")
                {
                    value = segment.Data;
                }
                else
                {
                    Binding binding = new Binding();
                    binding.Item = segment.Data;
                    value = binding.GetRefreshedValue(prop);
                }
            }
            else if (segment.Data == null && segment.CompiledData != null) {
                value = segment.CompiledData(prop);
            }
            if (IsSafe)
            {
                return value;
            }
            else
            {
                if (value is string)
                {
                    return RemoveHTMLTagsCompiled(value as string);
                }
                return value;
            }
        }
}
}