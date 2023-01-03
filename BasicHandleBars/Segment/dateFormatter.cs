using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicHandleBars.Segment
{
    public class dateFormatter : ValueSegment
    {
        public dateFormatter(string template) : base(template)
        {

        }
        public dateFormatter() : base("")
        {

        }
        public override void Compile(StringBuilder builder)
        {
            var Options = OptionsExtension.GetOptions<DateFormatterOptions>(this.OptionsInText);
            if (!string.IsNullOrEmpty(Options.value)) {
                var value = DataExtension.getValue(Options.value, this);
                DateTime dateValue = Convert.ToDateTime(value);
                builder.Append(dateValue.ToString(Options.format, CultureInfo.CreateSpecificCulture(Options.culture)));
            }
        }
    }
    public class DateFormatterOptions
    {
        public string format { get; set; } = "dd.MM.yyyy HH:mm";
        public string culture { get; set; } = "tr-TR";
        public string value { get; set; } = "";
    }
}
