using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicHandleBars.Segment
{
    public class ContainerSegment : Segment, IHasData
    {
        public ContainerSegment(string template) : base(template)
        {
            
        }
        public ContainerSegment() : base("")
        {
            
        }
        public object Data { get; set; }
        public Func<string, object> CompiledData { set; get; }
        List<Segment> _Segments = new List<Segment>();
        public List<Segment> Segments {
            get {
                return _Segments;
            }
        }
        public override void TemplatedChanged()
        {
            base.TemplatedChanged();
            this._Segments = SegmentExtension.Parse(this.Template);
        }
        public override void Compile(StringBuilder builder)
        {
            for (int i = 0; i < this.Segments.Count; i++)
            {
                this.Segments[i].Compile(builder);
            }
        }
    }
}
