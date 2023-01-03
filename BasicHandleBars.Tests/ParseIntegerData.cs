using System;
using BasicHandleBars.Segment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BasicHandleBars.Tests
{
    [TestClass]
    public class ParseIntegerData
    {
        [TestMethod]
        public void Parse_Integer_1()
        {
            var template = @"<div>{{value}}</div>";
            for (int i = 1; i < 10; i++)
            {
                var result = BasicHandleBars.Handlebars.Compile(new CompileInput
                {
                    template = template,
                    data = new { value = 100 * i }
                });
                if (result != string.Format("<div>{0}</div>", i * 100))
                {
                    Assert.Fail("Parse_Data_1 invalid" + result);
                }
            }
        }
        [TestMethod]
        public void Parse_Integer_2()
        {
            var template = @"<div>{{#numberFormatter [value=value] [format=N2]}}</div>";
            for (int i = 1; i < 10; i++)
            {
                var result = BasicHandleBars.Handlebars.Compile(new CompileInput
                {
                    template = template,
                    data = new { value = 100 * i }
                });
                if (result != string.Format("<div>{0}</div>", (i * 100).ToString("N2")))
                {
                    Assert.Fail("Parse_Data_1 invalid" + result);
                }
            }
        }
        [TestMethod]
        public void Parse_Integer_3()
        {
            var template = @"<div>{{#numberFormatter [value=value] [format=N0]}}</div>";
            for (int i = 1; i < 10; i++)
            {
                var result = BasicHandleBars.Handlebars.Compile(new CompileInput
                {
                    template = template,
                    data = new { value = 10000 * i }
                });
                if (result != string.Format("<div>{0}</div>", (i * 10000).ToString("N0")))
                {
                    Assert.Fail("Parse_Data_1 invalid" + result);
                }
            }
        }
        [TestMethod]
        public void Parse_Integer_4()
        {
            var template = @"<div>{{#numberFormatter [value=value] [format=C]}}</div>";
            for (int i = 1; i < 10; i++)
            {
                var result = BasicHandleBars.Handlebars.Compile(new CompileInput
                {
                    template = template,
                    data = new { value = 10000 * i }
                });
                if (result != string.Format("<div>{0}</div>", (i * 10000).ToString("C")))
                {
                    Assert.Fail("Parse_Data_1 invalid" + result);
                }
            }
        }
        [TestMethod]
        public void Parse_Integer_5()
        {
            var template = @"<div>{{#numberFormatter [value=value] [format=C]}}</div>";
            var result = BasicHandleBars.Handlebars.Compile(new CompileInput
            {
                template = template,
                data = new { value = "10000"}
            });
            if (result != string.Format("<div>{0}</div>", (10000).ToString("C")))
            {
                Assert.Fail("Parse_Data_1 invalid" + result);
            }
        }
    }
}
