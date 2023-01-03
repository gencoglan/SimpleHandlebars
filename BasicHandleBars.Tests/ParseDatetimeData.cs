using System;
using BasicHandleBars.Segment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BasicHandleBars.Tests
{
    [TestClass]
    public class ParseDatetimeData
    {
        [TestMethod]
        public void Parse_Datetime_1()
        {
            var template = @"<div>{{#dateFormatter [value=date] [format=dd.MM.yyyy]}}</div>";

            var result = BasicHandleBars.Handlebars.Compile(new CompileInput
            {
                template = template,
                data = new { date = new DateTime(2022, 10, 1, 10, 11, 10) }
            });
            if (result != "<div>01.10.2022</div>")
            {
                Assert.Fail("Parse_Data_1 invalid");
            }
        }
        [TestMethod]
        public void Parse_Datetime_2()
        {
            var template = @"<div>{{#dateFormatter [value=date] [format=d] [culture=tr-TR]}}</div>";

            var result = BasicHandleBars.Handlebars.Compile(new CompileInput
            {
                template = template,
                data = new { date = new DateTime(2022, 10, 1, 10, 11, 10) }
            });
            if (result != "<div>1.10.2022</div>")
            {
                Assert.Fail("Parse_Data_1 invalid" + result);
            }
        }
        [TestMethod]
        public void Parse_Datetime_3()
        {
            var template = @"<div>{{#dateFormatter [value=date] [format=dd.MM.yyyy HH:mm:ss] }}</div>";

            var result = BasicHandleBars.Handlebars.Compile(new CompileInput
            {
                template = template,
                data = new { date = new DateTime(2022,10,1,10,11,10) }
            });
            if (result != "<div>01.10.2022 10:11:10</div>") {
                Assert.Fail("Parse_Data_1 invalid");
            }

        }
        [TestMethod]
        public void Parse_Datetime_4()
        {
            var template = @"<div>{{#dateFormatter [value=date] [format=D] [culture=tr-TR]}}</div>";

            var result = BasicHandleBars.Handlebars.Compile(new CompileInput
            {
                template = template,
                data = new { date = new DateTime(2022, 10, 1, 10, 11, 10) }
            });
            if (result != "<div>1 Ekim 2022 Cumartesi</div>")
            {
                Assert.Fail("Parse_Data_1 invalid" + result);
            }
        }
        [TestMethod]
        public void Parse_Datetime_5()
        {
            var template = @"<div>{{#dateFormatter [value=date] [format=Y] [culture=tr-TR]}}</div>";

            var result = BasicHandleBars.Handlebars.Compile(new CompileInput
            {
                template = template,
                data = new { date = new DateTime(2022, 10, 1, 10, 11, 10) }
            });
            if (result != "<div>Ekim 2022</div>")
            {
                Assert.Fail("Parse_Data_1 invalid" + result);
            }
        }
    }
}

