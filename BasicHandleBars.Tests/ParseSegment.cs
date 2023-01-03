using System;
using BasicHandleBars.Segment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BasicHandleBars.Tests
{
    [TestClass]
    public class ParseSegment
    {
        [TestMethod]
        public void Parse_Segment_Test_1()
        {
            var template = @"<div>{{data}}<div>";
            var result = BasicHandleBars.Segment.SegmentExtension.Parse(template);
            if (result.Count != 3)
            {
                Assert.Fail("Compile_Test_1 segments is wrong");
            }
        }
        [TestMethod]
        public void Parse_Segment_Test_1_1()
        {
            var template = @"<div>{{{data}}}<div>";
            var result = BasicHandleBars.Segment.SegmentExtension.Parse(template);
            if (result.Count != 3)
            {
                Assert.Fail("Compile_Test_1 segments is wrong");
            }
        }
        [TestMethod]
        public void Parse_Segment_Test_2()
        {
            var template = @"<div>{{#dateFormatter [value=date] }}{{#region}}{{#region}}<div></div>{{/region}}{{/region}}</div>";
            for (int i = 0; i < 2; i++) {
                if (i == 1) {
                    template = @"<div>{{#dateFormatter [value=date]}}{{#region}}{{#region}}<div></div>{{/region}}{{/region}}</div>";
                }
                var result = BasicHandleBars.Segment.SegmentExtension.Parse(template);
                if (result.Count == 4)
                {
                    if (result[0].Template != "<div>")
                    {
                        Assert.Fail("Compile_Test segments is wrong");
                    }
                    if (result[1].Template != "")
                    {
                        Assert.Fail("Compile_Test segments is wrong");
                    }
                    if (result[1].OptionsInText.Trim() != "[value=date]")
                    {
                        Assert.Fail("Compile_Test segments is wrong");
                    }
                    if (result[2].Template != @"{{#region}}<div></div>{{/region}}")
                    {
                        Assert.Fail("Compile_Test segments is wrong");
                    }
                    if ((result[2] as ContainerSegment).Segments.Count != 1)
                    {
                        Assert.Fail("Compile_Test segments is wrong");
                    }
                    if ((result[2] as ContainerSegment).Segments[0].Template != "<div></div>")
                    {
                        Assert.Fail("Compile_Test segments is wrong");
                    }
                    if (result[3].Template != "</div>")
                    {
                        Assert.Fail("Compile_Test segments is wrong");
                    }
                }
                else
                {
                    Assert.Fail("Compile_Test segments is wrong");
                }
            }

            
        }
        [TestMethod]
        public void Parse_Segment_Test_3()
        {
            var template = @"<div>{{#dateFormatter}}{{#region}}{{#region}}<div></div>{{/region}}{{/region}}</div>";

            var result = BasicHandleBars.Segment.SegmentExtension.Parse(template);
            if (result.Count == 4)
            {
                if (result[0].Template != "<div>")
                {
                    Assert.Fail("Compile_Test segments is wrong");
                }
                if (result[1].Template != "")
                {
                    Assert.Fail("Compile_Test segments is wrong");
                }
                if (result[2].Template != @"{{#region}}<div></div>{{/region}}")
                {
                    Assert.Fail("Compile_Test segments is wrong");
                }
                if (result[2].Template != @"{{#region}}<div></div>{{/region}}")
                {
                    Assert.Fail("Compile_Test segments is wrong");
                }
                if ((result[2] as ContainerSegment).Segments.Count != 1)
                {
                    Assert.Fail("Compile_Test segments is wrong");
                }
                if ((result[2] as ContainerSegment).Segments[0].Template != "<div></div>")
                {
                    Assert.Fail("Compile_Test segments is wrong");
                }
                if (result[3].Template != "</div>")
                {
                    Assert.Fail("Compile_Test segments is wrong");
                }
            }
            else
            {
                Assert.Fail("Compile_Test segments is wrong");
            }
        }
    }
}

