using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicHandleBars.Segment
{
    public class each : ContainerSegment , IHasData
    {
        public each(string template) : base(template)
        {
            
        }
        public each() : base("")
        {

        }
        public override void Compile(StringBuilder builder)
        {
            var Options = OptionsExtension.GetOptions<EachFormatterOptions>(this.OptionsInText);

            var data = DataExtension.getValue(Options.value, this) as System.Collections.IEnumerable;
            foreach (var row in data) {
                for (int i = 0; i < this.Segments.Count; i++)
                {
                    if (this.Segments[i] is IHasData)
                    {
                        ((IHasData)this.Segments[i]).Data = row;
                        ((IHasData)this.Segments[i]).CompiledData = CompiledData;
                    }
                    this.Segments[i].Compile(builder);
                }
            }
        }
    }
    public class EachFormatterOptions
    {
        public string value { get; set; } = "";
    }
}
