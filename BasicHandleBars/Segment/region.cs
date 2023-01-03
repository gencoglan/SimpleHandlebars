using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicHandleBars.Segment
{
    public class region : ContainerSegment , IHasData
    {
        public region(string template) : base(template)
        {

        }
        public region():base("")
        {

        }
    }
}
