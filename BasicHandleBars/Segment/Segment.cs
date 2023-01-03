using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicHandleBars.Segment
{
    public abstract class Segment : ISegment
    {
        Segment() {

        }
        public Segment(string template)
        {
            this.Template = template;
        }
        public string OptionsInText { get; set; }
        private string sTemplate = "";
        public string Template {
            get {
                return sTemplate;
            }
            set {
                bool callTemplateChange = false;
                if (sTemplate != value) {
                    callTemplateChange = true;
                }
                sTemplate = value;
                if (callTemplateChange) {
                    TemplatedChanged();
                }
            }
        }
        public virtual void TemplatedChanged() {

        }
        public abstract void Compile(StringBuilder builder);
    }
}
