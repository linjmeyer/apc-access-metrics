using ApcAccessMetrcs.Common.Apc;
using Xunit;
using ApcAccessMetrics.Common.Extensions;

namespace ApcAccessMetrics.Common.Tests
{
    public class ExtensionTests
    {
        [Fact]
        public void TestObjectGetMetricTags()
        {
            var status = new ApcStatus()
            {
                Name = "name",
                Model = "model"
            };

            var tags = status.GetMetricTags();
            Assert.True(tags.ContainsKey("Name"));
            Assert.True(tags["Name"] == "name");

            Assert.True(tags.ContainsKey("Model"));
            Assert.True(tags["Model"] == "model");
        }
    }
}