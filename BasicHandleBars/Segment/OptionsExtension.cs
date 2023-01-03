using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BasicHandleBars.Segment
{
    public static class OptionsExtension
    {
        private const string PropertiesRegex = "\\[?/?[A-Za-z0-9 *.!:=',\\-]+\\]";
        public static T GetOptions<T>(string text) where T :new() {
            var options = new T();
            if (text != null) {
                Regex containerRegex = new Regex(PropertiesRegex);
                var containers = containerRegex.Matches(text);
                Match container = null;
                for (int i = 0; i < containers.Count; i++)
                {
                    container = containers[i];
                    try
                    {
                        var propAndValue = container.Value.Split('=');
                        var propName = propAndValue[0].Replace("[","");
                        var value = propAndValue[1].Replace("]", "");
                        PropertyInfo prop = options.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                        if (null != prop && prop.CanWrite)
                        {
                            prop.SetValue(options, value, null);
                        }
                    }
                    catch
                    {

                    }
                }
            }
            return options;
        }
    }
}
