using BasicHandleBars.Segment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicHandleBars
{
    public static class Handlebars
    {
        public static string Compile(CompileInput input) {
            using (var context = new Context(input)) {
                return context.Compile();
            }
        }
    }
    public class CompileInput {
        public string template { get; set; }
        public object data { get; set; }
        public Func<string,object> compiledData { set; get; }
    }
}
