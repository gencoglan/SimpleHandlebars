using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicHandleBars.Segment
{
    public class Context : IDisposable {
        private string Template { get; set; }
        private object Data { get; set; }
        private Func<string, object> CompiledData { set; get; }
        private List<Segment> Segments { get; set; }
        public Context(CompileInput input) {
            this.Template = input.template;
            this.Data = input.data;
            this.CompiledData = input.compiledData;
            this.Segments = BasicHandleBars.Segment.SegmentExtension.Parse(this.Template);
        }
        public string Compile() {   
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < this.Segments.Count; i++)
            {
                if (this.Segments[i] is IHasData)
                {
                    ((IHasData)this.Segments[i]).Data = Data;
                    ((IHasData)this.Segments[i]).CompiledData = CompiledData;
                }
                this.Segments[i].Compile(stringBuilder);
            }
            return stringBuilder.ToString();
        }
        void IDisposable.Dispose()
        {
        
        }
    }

}
