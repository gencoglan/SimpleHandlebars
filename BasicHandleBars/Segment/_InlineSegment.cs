using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicHandleBars.Segment
{
    public class InlineSegment: Segment
    {
        public InlineSegment(string template):base(template) {
            
        }
        public InlineSegment() : base("")
        {

        }
        public override void Compile(StringBuilder builder)
        {
            builder.Append(this.Template);
        }
    }
}
