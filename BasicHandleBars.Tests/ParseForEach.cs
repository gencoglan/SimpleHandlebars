using System;
using BasicHandleBars.Segment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BasicHandleBars.Tests
{
    [TestClass]
    public class ParseForEach
    {
        [TestMethod]
        public void Parse_Each_1()
        {
            var template = @"{{#each [value=data]}}{{this}}{{/each}}";
            int[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var result = BasicHandleBars.Handlebars.Compile(new CompileInput
            {
                template = template,
                data = new {
                    data
                }
            });
            if (result != "12345678910")
            {
                Assert.Fail("Parse_Data_1 invalid" + result);
            }
        }
        [TestMethod]
        public void Parse_Each_2()
        {
            var template = @"{{#each [value=data]}}{{value}}{{/each}}";

            object[] data = { new { value = 0 }, new { value = 1 } };

            var result = BasicHandleBars.Handlebars.Compile(new CompileInput
            {
                template = template,
                data = new
                {
                    data
                }
            });
            if (result != "01")
            {
                Assert.Fail("Parse_Data_1 invalid" + result);
            }
        }
    }


}
