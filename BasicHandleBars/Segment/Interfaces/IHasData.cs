using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicHandleBars.Segment
{
    public interface IHasData
    {
        object Data { get; set; }
        Func<string, object> CompiledData { set; get; }
    }
}
