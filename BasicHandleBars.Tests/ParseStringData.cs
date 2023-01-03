using System;
using BasicHandleBars.Segment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BasicHandleBars.Tests
{
    [TestClass]
    public class ParseStringData
    {
        [TestMethod]
        public void Parse_String_1()
        {
            var template = @"<div>{{data}}</div>";
            var result = BasicHandleBars.Handlebars.Compile(new CompileInput
            {
                template = template,
                data = new { data = "<div style='padding-top:10px;'>Test</div>" }
            });
            if (result != "<div>Test</div>")
            {
                Assert.Fail("Parse_Data_1 invalid" + result);
            }
        }

        [TestMethod]
        public void Parse_String_2()
        {
            var template = @"<div>{{{data}}}</div>";
            var result = BasicHandleBars.Handlebars.Compile(new CompileInput
            {
                template = template,
                data = new { data = "<div style='padding-top:10px;'>Test</div>" }
            });
            if (result != "<div><div style='padding-top:10px;'>Test</div></div>")
            {
                Assert.Fail("Parse_Data_1 invalid" + result);
            }
        }
    }
}

