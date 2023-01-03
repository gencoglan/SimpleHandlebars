using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BasicHandleBars.Segment
{
    public static class SegmentExtension
    {
        const string ValueRegexString = "{{{?#?/?[A-Za-z0-9 *.:!='\\-,\\[\\]]+}}}?";
        public static List<Segment> Parse(string template) {
            var list = new List<Segment>();
            Regex containerRegex = new Regex(ValueRegexString);
            var containers = containerRegex.Matches(template);
            var lastIndexToRead = 0;
            var subTemplate = "";
            Match container = null;
            for (int i = 0; i < containers.Count; i++) {
                container = containers[i];
                if (container.Index > lastIndexToRead) {
                    subTemplate = template.Substring(lastIndexToRead, container.Index - lastIndexToRead);
                    var inlineContext = new InlineSegment(subTemplate);
                    lastIndexToRead = container.Index;
                    list.Add(inlineContext);
                }
                if (container.Value.StartsWith("{{#"))
                {
                    var subTemplateStartIndex = container.Index + container.Length;
                    var openingTag = GetOpeningTag(container.Value);
                    var closingTag = openingTag.Replace("{{#", "{{/");
                    var closingTagCount = 1;
                    subTemplate = "";
                    var found = false;
                    for (int j = i + 1; j < containers.Count; j++)
                    {
                        var tempContainer = containers[j];
                        if (tempContainer.Value.Trim().StartsWith(closingTag))
                        {
                            closingTagCount -= 1;
                        }
                        else if (GetOpeningTag(tempContainer.Value) == openingTag)
                        {
                            closingTagCount += 1;
                        }
                        if (closingTagCount == 0)
                        {
                            var subTemplateEndIndex = tempContainer.Index - subTemplateStartIndex;
                            subTemplate = template.Substring(subTemplateStartIndex, subTemplateEndIndex);
                            var name = openingTag.Replace("{{#", "").Replace("}}", "");
                            var containerSegment = SegmentFactory.getSegment(name);
                            (containerSegment as Segment).OptionsInText = GetOptionsInText(container.Value);
                            lastIndexToRead = tempContainer.Index + tempContainer.Length;
                            (containerSegment as Segment).Template = subTemplate;
                            list.Add(containerSegment as Segment);
                            i = j;
                            found = true;
                            break;
                        }
                    }
                    if (!found) {
                        var name = openingTag.Replace("{{#", "").Replace("}}", "");
                        var containerSegment = SegmentFactory.getSegment(name);
                        var Segment = containerSegment as Segment;
                        Segment.Template = "";
                        Segment.OptionsInText = GetOptionsInText(container.Value);
                        list.Add(Segment);
                        lastIndexToRead = container.Index + container.Length;
                    }
                }
                else if (container.Value.StartsWith("{{/")) {
                    continue;
                }
                else
                {
                    var valueSegment = new ValueSegment(container.Value);
                    valueSegment.OptionsInText = "";
                    list.Add(valueSegment);
                    lastIndexToRead = container.Index + container.Length;

                }
            }
            if (container == null)
            {
                var inlineContext = new InlineSegment(template);
                list.Add(inlineContext);
            }
            else if (lastIndexToRead < template.Length) {
                subTemplate = template.Substring(lastIndexToRead);
                var inlineContext = new InlineSegment(subTemplate);
                list.Add(inlineContext);
            }
            return list;
        }
        private static string GetOpeningTag(string tag) {
            var values = tag.Trim().Split(' ');
            return values[0];
        }
        private static string GetOptionsInText(string tag)
        {
            var values = tag.Trim().Split(' ');
            values[0] = "";
            return string.Join(" ", values).Replace("}}","");
        }
    }
}
