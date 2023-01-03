using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicHandleBars.Segment
{
    public class numberFormatter: ValueSegment
    {
        public numberFormatter(string template) : base(template)
        {

        }
        public numberFormatter() : base("")
        {

        }
        public override void Compile(StringBuilder builder)
        {
            var Options = OptionsExtension.GetOptions<NumberFormatterOptions>(this.OptionsInText);
            if (!string.IsNullOrEmpty(Options.value))
            {
                var value = DataExtension.getValue(Options.value, this);
                Decimal dateValue = Convert.ToDecimal(value);
                builder.Append(dateValue.ToString(Options.format, CultureInfo.CreateSpecificCulture(Options.culture)));
            }
        }
    }
    public class NumberFormatterOptions
    {
        public string format { get; set; } = "N2";
        public string culture { get; set; } = "tr-TR";
        public string value { get; set; } = "";
    }
}
