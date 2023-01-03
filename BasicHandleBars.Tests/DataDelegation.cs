using System;
using BasicHandleBars.Segment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BasicHandleBars.Tests
{
    [TestClass]
    public class DataDelegation
    {
        [TestMethod]
        public void Parse_DataDelegation_1()
        {
            var template = @"<div>{{data}}</div>";
            var result = BasicHandleBars.Handlebars.Compile(new CompileInput
            {
                template = template,
                compiledData = (prop) => {
                    if (prop == "data") {
                        return "<div><div style='padding-top:10px;'>Test</div></div>";
                    }
                    return null;
                }
            });
            if (result != "<div>Test</div>")
            {
                Assert.Fail("Parse_Data_1 invalid" + result);
            }
        }

        [TestMethod]
        public void Parse_DataDelegation_2()
        {
            var template = @"<div>{{{data}}}</div>";
            var result = BasicHandleBars.Handlebars.Compile(new CompileInput
            {
                template = template,
                compiledData = (prop) => {
                    if (prop == "data")
                    {
                        return "<div style='padding-top:10px;'>Test</div>";
                    }
                    return null;
                }
            });
            if (result != "<div><div style='padding-top:10px;'>Test</div></div>")
            {
                Assert.Fail("Parse_Data_1 invalid" + result);
            }
        }
    }
}

