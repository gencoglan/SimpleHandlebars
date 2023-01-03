using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicHandleBars.Segment
{
    public class ValueSegment : Segment , IHasData
    {
        public ValueSegment(string template) : base(template)
        {

        }
        public ValueSegment() : base("")
        {

        }
        public object Data { get; set; }
        public Func<string, object> CompiledData { set; get; }
        
        public override void Compile(StringBuilder builder)
        {
            var value = DataExtension.getValue(this.Template, this);
            builder.Append(value);
        }
    }
}
